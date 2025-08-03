namespace KDramaSystem.Application.UseCases.Ator.Criar;

public class CriarAtorRequest
{
    public string Nome { get; set; } = null!;
    public string? NomeCompleto { get; set; }
    public int? AnoNascimento { get; set; }
    public decimal? Altura { get; set; }
    public string? Pais { get; set; }
    public string? Biografia { get; set; }
    public string? FotoUrl { get; set; }
    public string? Instagram { get; set; }
}