namespace KDramaSystem.Application.DTOs.Temporada;

public class CriarTemporadaDto
{
    public string Nome { get; set; } = string.Empty;
    public int Ordem { get; set; }
    public Guid DoramaId { get; set; }
    public DateTime DataEstreia { get; set; }
    public DateTime? DataFim { get; set; }
}