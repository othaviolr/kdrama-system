namespace KDramaSystem.Application.UseCases.ListaPrateleira.Excluir;

public class ExcluirListaPrateleiraRequest
{
    public Guid ListaId { get; set; }
    public Guid UsuarioId { get; set; }
}