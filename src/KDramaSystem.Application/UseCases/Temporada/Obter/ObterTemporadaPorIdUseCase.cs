using KDramaSystem.Application.DTOs.Temporada;
using KDramaSystem.Application.DTOs.Dorama;
using KDramaSystem.Application.DTOs.Episodio;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Temporada.Obter;

public class ObterTemporadaPorIdUseCase
{
    private readonly ITemporadaRepository _temporadaRepository;

    public ObterTemporadaPorIdUseCase(ITemporadaRepository temporadaRepository)
    {
        _temporadaRepository = temporadaRepository;
    }

    public async Task<ObterTemporadaDto?> ExecutarAsync(ObterTemporadaPorIdRequest request)
    {
        var temporada = await _temporadaRepository.ObterPorIdAsync(request.Id);
        if (temporada == null)
            return null;

        ObterDoramaDto? doramaDto = null;
        if (temporada.Dorama != null)
        {
            doramaDto = new ObterDoramaDto
            {
                DoramaId = temporada.Dorama.Id,
                Titulo = temporada.Dorama.Titulo,
                TituloOriginal = temporada.Dorama.TituloOriginal,
                Sinopse = temporada.Dorama.Sinopse,
                CapaUrl = temporada.Dorama.ImagemCapaUrl,
                AnoLancamento = temporada.Dorama.AnoLancamento,
                PaisOrigem = temporada.Dorama.PaisOrigem,
                EmExibicao = temporada.Dorama.EmExibicao,
                Plataforma = temporada.Dorama.Plataforma,
                Generos = temporada.Dorama.Generos
                    .Select(g => new ObterDoramaDto.GeneroDto
                    {
                        Id = g.Id,
                        Nome = g.Nome
                    }).ToList(),
                Atores = temporada.Dorama.Atores
                    .Where(da => da.Ator != null)
                    .Select(da => new ObterDoramaDto.AtorDto
                    {
                        Id = da.Ator!.Id,
                        Nome = da.Ator.Nome
                    }).ToList()
            };
        }

        var episodiosDto = (temporada.Episodios ?? Enumerable.Empty<Domain.Entities.Episodio>())
            .OrderBy(e => e.Numero)
            .Select(e => new ObterEpisodioDto
            {
                Id = e.Id,
                TemporadaId = e.TemporadaId,
                Numero = e.Numero,
                Titulo = e.Titulo,
                DuracaoMinutos = e.DuracaoMinutos,
                Sinopse = e.Sinopse
            }).ToList();

        return new ObterTemporadaDto
        {
            Id = temporada.Id,
            Nome = temporada.Nome,
            Ordem = temporada.Numero,
            DoramaId = temporada.DoramaId,
            Dorama = doramaDto,
            DataEstreia = new DateTime(temporada.AnoLancamento, 1, 1), 
            Episodios = episodiosDto,
        };
    }
}