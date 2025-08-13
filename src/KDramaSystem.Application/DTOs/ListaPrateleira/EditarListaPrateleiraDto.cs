namespace KDramaSystem.Application.DTOs.ListaPrateleira;

public class EditarListaPrateleiraDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string? Descricao { get; set; }
    public string? ImagemCapaUrl { get; set; }
}