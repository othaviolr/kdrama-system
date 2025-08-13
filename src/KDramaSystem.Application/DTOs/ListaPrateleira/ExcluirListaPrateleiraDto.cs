namespace KDramaSystem.Application.DTOs.ListaPrateleira;

public class ExcluirListaPrateleiraDto
{
    public Guid Id { get; set; }
    public string Mensagem { get; set; } = "Lista excluída com sucesso.";
}