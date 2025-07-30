namespace KDramaSystem.Application.DTOs.Dorama;

public class EditarDoramaDto
{
    public Guid DoramaId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Sinopse { get; set; } = string.Empty;
    public string CapaUrl { get; set; } = string.Empty;
    public int AnoLancamento { get; set; }
    public string PaisOrigem { get; set; } = string.Empty;
    public string Plataforma { get; set; } = string.Empty;
    public string ElencoPrincipal { get; set; } = string.Empty;
}