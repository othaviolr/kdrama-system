namespace KDramaSystem.Application.DTOs.Temporada;

public class EditarTemporadaDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public int Ordem { get; set; }
    public DateTime DataEstreia { get; set; }
    public DateTime? DataFim { get; set; }
}