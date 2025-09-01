using KDramaSystem.Application.DTOs.Dorama;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Dorama.Obter;

public class ObterDoramaUseCase
{
    private readonly IDoramaRepository _doramaRepository;

    public ObterDoramaUseCase(IDoramaRepository doramaRepository)
    {
        _doramaRepository = doramaRepository;
    }

    public async Task<ObterDoramaDto?> ExecutarAsync(ObterDoramaRequest request)
    {
        var dorama = await _doramaRepository.ObterPorIdAsync(request.Id);
        if (dorama == null)
            return null;

        return new ObterDoramaDto
        {
            DoramaId = dorama.Id,
            Titulo = dorama.Titulo,
            TituloOriginal = dorama.TituloOriginal,
            Sinopse = dorama.Sinopse,
            CapaUrl = dorama.ImagemCapaUrl,
            AnoLancamento = dorama.AnoLancamento,
            PaisOrigem = dorama.PaisOrigem,
            EmExibicao = dorama.EmExibicao,
            Plataforma = dorama.Plataforma,
            Generos = dorama.Generos.Select(g => new ObterDoramaDto.GeneroDto
            {
                Id = g.Id,
                Nome = g.Nome
            }).ToList(),
            Atores = dorama.Atores
                .Where(da => da.Ator != null)
                .Select(da => new ObterDoramaDto.AtorDto
                {
                    Id = da.Ator.Id,
                    Nome = da.Ator.Nome
                }).ToList()
        };
    }

    public async Task<ObterDoramaDto?> ExecutarPorTituloAsync(string titulo)
    {
        var dorama = await _doramaRepository.ObterPorTituloAsync(titulo);
        if (dorama == null)
            return null;

        return new ObterDoramaDto
        {
            DoramaId = dorama.Id,
            Titulo = dorama.Titulo,
            TituloOriginal = dorama.TituloOriginal,
            Sinopse = dorama.Sinopse,
            CapaUrl = dorama.ImagemCapaUrl,
            AnoLancamento = dorama.AnoLancamento,
            PaisOrigem = dorama.PaisOrigem,
            EmExibicao = dorama.EmExibicao,
            Plataforma = dorama.Plataforma,
            Generos = new List<ObterDoramaDto.GeneroDto>(),
            Atores = new List<ObterDoramaDto.AtorDto>()     
        };
    }
}