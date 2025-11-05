using KDramaSystem.Application.Common;
using KDramaSystem.Application.DTOs.Dorama;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Ator.Obter;

public class ObterAtorUseCase
{
    private readonly IAtorRepository _repository;

    public ObterAtorUseCase(IAtorRepository repository)
    {
        _repository = repository;
    }

    public async Task<AtorResponse> ExecutarAsync(ObterAtorRequest request)
    {
        var ator = await _repository.ObterPorIdAsync(request.Id);
        if (ator == null)
            throw new KeyNotFoundException("Ator não encontrado");

        return MapearParaResponse(ator);
    }

    public async Task<AtorResponse?> ExecutarPorNomeAsync(string nome)
    {
        var ator = await _repository.ObterPorNomeAsync(nome);
        if (ator == null) return null;

        return MapearParaResponse(ator);
    }

    public async Task<PaginacaoResponse<AtorResumoResponse>> ExecutarPaginadoAsync(PaginacaoRequest paginacao)
    {
        var totalItens = await _repository.ContarAsync();
        var atores = await _repository.ObterPaginadoAsync(paginacao.Skip, paginacao.TamanhoPagina);

        return new PaginacaoResponse<AtorResumoResponse>
        {
            Itens = atores.Select(MapearParaResumo).ToList(),
            PaginaAtual = paginacao.Pagina,
            TamanhoPagina = paginacao.TamanhoPagina,
            TotalItens = totalItens
        };
    }

    public async Task<PaginacaoResponse<AtorResponse>> ExecutarPaginadoCompletoAsync(PaginacaoRequest paginacao)
    {
        var totalItens = await _repository.ContarAsync();
        var atores = await _repository.ObterPaginadoAsync(paginacao.Skip, paginacao.TamanhoPagina);

        return new PaginacaoResponse<AtorResponse>
        {
            Itens = atores.Select(MapearParaResponse).ToList(),
            PaginaAtual = paginacao.Pagina,
            TamanhoPagina = paginacao.TamanhoPagina,
            TotalItens = totalItens
        };
    }

    private AtorResponse MapearParaResponse(Domain.Entities.Ator ator)
    {
        return new AtorResponse
        {
            Id = ator.Id,
            Nome = ator.Nome,
            NomeCompleto = ator.NomeCompleto,
            AnoNascimento = ator.AnoNascimento,
            Altura = ator.Altura,
            Pais = ator.Pais,
            Biografia = ator.Biografia,
            FotoUrl = ator.FotoUrl,
            Instagram = ator.Instagram,
            Doramas = ator.Doramas
                .Where(da => da.Dorama != null)
                .Select(da => new DoramaResumoDto
                {
                    DoramaId = da.Dorama.Id,
                    Titulo = da.Dorama.Titulo,
                    TituloOriginal = da.Dorama.TituloOriginal,
                    CapaUrl = da.Dorama.ImagemCapaUrl,
                    AnoLancamento = da.Dorama.AnoLancamento
                })
                .OrderByDescending(d => d.AnoLancamento)
                .ToList()
        };
    }

    private AtorResumoResponse MapearParaResumo(Domain.Entities.Ator ator)
    {
        return new AtorResumoResponse
        {
            Id = ator.Id,
            Nome = ator.Nome,
            FotoUrl = ator.FotoUrl
        };
    }

    public async Task<List<Domain.Entities.Ator>> ExecutarTodosAsync()
    {
        return await _repository.ObterTodosAsync();
    }
}