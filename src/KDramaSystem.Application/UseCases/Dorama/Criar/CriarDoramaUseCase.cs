using KDramaSystem.Application.UseCases.Dorama.Criar;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Enums;
using KDramaSystem.Domain.Interfaces.Repositories;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Dorama;

public class CriarDoramaUseCase
{
    private readonly IDoramaRepository _doramaRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IGeneroRepository _generoRepository;
    private readonly IAtorRepository _atorRepository;

    public CriarDoramaUseCase(
        IDoramaRepository doramaRepository,
        IUnitOfWork unitOfWork,
        IUsuarioRepository usuarioRepository,
        IGeneroRepository generoRepository,
        IAtorRepository atorRepository)
    {
        _doramaRepository = doramaRepository;
        _unitOfWork = unitOfWork;
        _usuarioRepository = usuarioRepository;
        _generoRepository = generoRepository;
        _atorRepository = atorRepository;
    }

    public async Task<Guid> ExecutarAsync(CriarDoramaRequest request)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(request.UsuarioCriadorId);
        if (usuario == null)
            throw new Exception("Usuário não encontrado.");

        if (request.GeneroIds == null || !request.GeneroIds.Any())
            throw new Exception("É necessário informar pelo menos um gênero.");

        if (request.AtorIds == null || !request.AtorIds.Any())
            throw new Exception("É necessário informar pelo menos um ator.");

        var generos = await _generoRepository.ObterPorIdsAsync(request.GeneroIds);
        if (generos.Count != request.GeneroIds.Count())
            throw new Exception("Um ou mais gêneros informados são inválidos.");

        var atores = await _atorRepository.ObterPorIdsAsync(request.AtorIds);
        if (atores.Count != request.AtorIds.Count())
            throw new Exception("Um ou mais atores informados são inválidos.");

        var dorama = new KDramaSystem.Domain.Entities.Dorama(
            id: Guid.NewGuid(),
            usuarioId: request.UsuarioCriadorId,
            titulo: request.Titulo,
            paisOrigem: request.PaisOrigem,
            anoLancamento: request.AnoLancamento,
            emExibicao: request.EmExibicao,
            plataforma: (PlataformaStreaming)request.Plataforma,
            generos: generos,
            imagemCapaUrl: request.ImagemCapaUrl,
            sinopse: request.Sinopse,
            tituloOriginal: request.TituloOriginal
        );

        foreach (var genero in generos)
        {
            dorama.AdicionarGenero(genero);
        }

        foreach (var ator in atores)
        {
            var doramaAtor = new DoramaAtor(dorama.Id, ator.Id);
            dorama.AdicionarAtor(doramaAtor);
        }

        await _doramaRepository.AdicionarAsync(dorama);
        await _unitOfWork.SalvarAlteracoesAsync();

        return dorama.Id;
    }
}