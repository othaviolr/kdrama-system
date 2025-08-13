using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Application.UseCases.ListaPrateleira.Editar;

public class EditarListaPrateleiraRequest
{
    public Guid UsuarioId { get; set; }
    public Guid ListaId { get; set; }
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public string? ImagemCapaUrl { get; set; }
    public ListaPrivacidade? Privacidade { get; set; }
}