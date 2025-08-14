using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Application.DTOs.ListaPrateleira;

public class CriarListaPrateleiraRequestDto
{
    public string Nome { get; set; } = null!;
    public string? Descricao { get; set; }
    public string? ImagemCapaUrl { get; set; }
    public ListaPrivacidade Privacidade { get; set; }
}