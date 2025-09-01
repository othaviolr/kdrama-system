using KDramaSystem.Application.DTOs.Temporada;
using KDramaSystem.Application.DTOs.Dorama;
using KDramaSystem.Application.DTOs.Episodio;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Temporada.Obter;

public class ObterTemporadasPorNomeDoramaUseCase
{
    private readonly ITemporadaRepository _temporadaRepository;

    public ObterTemporadasPorNomeDoramaUseCase(ITemporadaRepository temporadaRepository)
    {
        _temporadaRepository = temporadaRepository;
    }

    public async Task<List<ObterTemporadaDto>> ExecutarAsync(string nomeDorama)
    {
        var temporadas = await _temporadaRepository.ObterPorNomeDoramaAsync(nomeDorama);
        if (temporadas == null || !temporadas.Any())
            return new List<ObterTemporadaDto>();

        var listaDto = new List<ObterTemporadaDto>();

        foreach (var temporada in temporadas)
        {
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

            listaDto.Add(new ObterTemporadaDto
            {
                Id = temporada.Id,
                Nome = temporada.Nome,
                Ordem = temporada.Numero,
                DoramaId = temporada.DoramaId,
                DataEstreia = new DateTime(temporada.AnoLancamento, 1, 1),
                Episodios = episodiosDto,
            });
        }

        return listaDto;
    }
}