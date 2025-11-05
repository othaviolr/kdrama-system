using KDramaSystem.Application.DTOs.Dorama;

namespace KDramaSystem.Application.UseCases.Ator.Obter;

public class AtorResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string? NomeCompleto { get; set; }
    public int? AnoNascimento { get; set; }
    public decimal? Altura { get; set; }
    public string? Pais { get; set; }
    public string? Biografia { get; set; }
    public string? FotoUrl { get; set; }
    public string? Instagram { get; set; }
    public List<DoramaResumoDto> Doramas { get; set; } = new();
}

public class AtorResumoResponse
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string? FotoUrl { get; set; }
}