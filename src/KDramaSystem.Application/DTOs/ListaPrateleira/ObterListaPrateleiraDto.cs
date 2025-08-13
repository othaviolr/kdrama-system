using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Application.DTOs.ListaPrateleira;

public class ObterListaPrateleiraDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; } = null!;
    public string? Descricao { get; set; }
    public string? ImagemCapaUrl { get; set; }
    public ListaPrivacidade Privacidade { get; set; }
    public string? ShareToken { get; set; }
    public Guid UsuarioId { get; set; }
    public DateTime DataCriacao { get; set; }
    public List<DoramaListaDto> Doramas { get; set; } = new();
}

public class DoramaListaDto
{
    public Guid DoramaId { get; set; }
    public DateTime DataAdicao { get; set; }
}