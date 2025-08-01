using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Application.DTOs.Episodio;

public class ObterEpisodioDto
{
    public Guid Id { get; set; }
    public Guid TemporadaId { get; set; }
    public int Numero { get; set; }
    public string Titulo { get; set; } = null!;
    public int DuracaoMinutos { get; set; }
    public TipoEpisodio Tipo { get; set; }
    public string? Sinopse { get; set; }
}