namespace KDramaSystem.Application.UseCases.ListaPrateleira.Obter;

public class ObterListaPrateleiraRequest
{
    public Guid? ListaId { get; set; }
    public Guid? UsuarioId { get; set; }
    public string? ShareToken { get; set; }
    public Guid? UsuarioLogadoId { get; set; }
    public bool ApenasDoUsuario { get; set; } = false;
}