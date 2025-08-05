using System.Collections.Generic;
using KDramaSystem.Application.DTOs.Ator;
using KDramaSystem.Application.DTOs.Episodio;
using KDramaSystem.Application.DTOs.Genero;
using KDramaSystem.Application.DTOs.Temporada;
using KDramaSystem.Domain.Enums;
using static KDramaSystem.Application.DTOs.Dorama.ObterDoramaDto;

namespace KDramaSystem.Application.DTOs.Dorama;

public class DoramaCompletoDto
{
    public Guid DoramaId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? TituloOriginal { get; set; }
    public string Sinopse { get; set; } = string.Empty;
    public string CapaUrl { get; set; } = string.Empty;
    public int AnoLancamento { get; set; }
    public string PaisOrigem { get; set; } = string.Empty;
    public bool EmExibicao { get; set; }
    public PlataformaStreaming Plataforma { get; set; }

    public List<ObterGeneroDto> Generos { get; set; } = new();
    public List<ObterAtorDto> Atores { get; set; } = new();

    public List<TemporadaCompletaDto> Temporadas { get; set; } = new();

    public class TemporadaCompletaDto : ObterTemporadaDto
    {
        public List<ObterEpisodioDto> Episodios { get; set; } = new();
    }
}
