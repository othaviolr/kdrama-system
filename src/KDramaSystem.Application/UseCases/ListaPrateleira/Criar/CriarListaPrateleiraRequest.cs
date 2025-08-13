using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Application.UseCases.ListaPrateleira.Criar;

public class CriarListaPrateleiraRequest
{
    public Guid UsuarioId { get; set; }
    public string Nome { get; set; } = null!;
    public string? Descricao { get; set; }
    public string? ImagemCapaUrl { get; set; }
    public ListaPrivacidade Privacidade { get; set; }
    
}