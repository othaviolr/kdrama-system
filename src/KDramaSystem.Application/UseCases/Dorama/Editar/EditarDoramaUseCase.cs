using KDramaSystem.Application.Exceptions;
using KDramaSystem.Application.UseCases.Dorama.Editar;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Enums;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.Dorama;

public class EditarDoramaUseCase
{
    private readonly IDoramaRepository _doramaRepository;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IGeneroRepository _generoRepository;

    public EditarDoramaUseCase(
        IDoramaRepository doramaRepository,
        IUsuarioRepository usuarioRepository,
        IGeneroRepository generoRepository)
    {
        _doramaRepository = doramaRepository;
        _usuarioRepository = usuarioRepository;
        _generoRepository = generoRepository;
    }

    public async Task ExecutarAsync(EditarDoramaRequest request)
    {
        var usuario = await _usuarioRepository.ObterPorIdAsync(request.UsuarioEditorId);
        if (usuario is null)
            throw new NotFoundException("Usuário não encontrado.");

        var dorama = await _doramaRepository.ObterPorIdAsync(request.DoramaId);
        if (dorama is null)
            throw new NotFoundException("Dorama não encontrado.");

        if (dorama.UsuarioId != request.UsuarioEditorId)
            throw new UnauthorizedAccessException("Acesso negado.");

        List<Genero> generos = dorama.Generos.ToList();
        if (request.GeneroIds != null && request.GeneroIds.Any())
        {
            var generosConsultados = await _generoRepository.ObterPorIdsAsync(request.GeneroIds);
            if (generosConsultados.Count != request.GeneroIds.Count)
                throw new Exception("Um ou mais gêneros informados são inválidos.");
            generos = generosConsultados;
        }

        dorama.AtualizarInformacoes(
        titulo: request.Titulo ?? dorama.Titulo,
        tituloOriginal: request.TituloOriginal ?? dorama.TituloOriginal,
        paisOrigem: request.PaisOrigem ?? dorama.PaisOrigem,
        anoLancamento: request.AnoLancamento ?? dorama.AnoLancamento,
        emExibicao: request.EmExibicao ?? dorama.EmExibicao,
        plataforma: request.Plataforma.HasValue
        ? (PlataformaStreaming)request.Plataforma.Value
        : dorama.Plataforma,
        generos: generos,
        imagemCapaUrl: request.ImagemCapaUrl ?? dorama.ImagemCapaUrl,
        sinopse: request.Sinopse ?? dorama.Sinopse
        );

        await _doramaRepository.AtualizarAsync(dorama);
    }
}