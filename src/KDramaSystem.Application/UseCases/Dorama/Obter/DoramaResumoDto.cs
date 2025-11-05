namespace KDramaSystem.Application.DTOs.Dorama;

public class DoramaResumoDto
{
    public Guid DoramaId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? TituloOriginal { get; set; }
    public string CapaUrl { get; set; } = string.Empty;
    public int AnoLancamento { get; set; }
}