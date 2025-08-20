using KDramaSystem.Application.DTOs.Dorama;
using KDramaSystem.Application.DTOs.Episodio;

namespace KDramaSystem.Application.DTOs.Temporada;

public class ObterTemporadaDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ordem { get; set; }
    public Guid DoramaId { get; set; }
    public DateTime DataEstreia { get; set; }
    public DateTime? DataFim { get; set; }
    public ObterDoramaDto? Dorama { get; set; }
    public List<ObterEpisodioDto> Episodios { get; set; } = new();
    public int NumeroEpisodios => Episodios?.Count ?? 0;
}