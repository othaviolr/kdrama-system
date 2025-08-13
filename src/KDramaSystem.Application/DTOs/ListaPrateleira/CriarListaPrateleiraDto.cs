namespace KDramaSystem.Application.DTOs.ListaPrateleira;

public class CriarListaPrateleiraDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string? Descricao { get; set; }
    public string? ImagemCapaUrl { get; set; }
    public string? ShareToken { get; set; }
}