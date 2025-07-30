namespace KDramaSystem.Application.UseCases.Dorama.Editar;

public class EditarDoramaRequest
{
    public Guid DoramaId { get; set; }
    public Guid UsuarioEditorId { get; set; }
    public string? Titulo { get; set; }
    public string? TituloOriginal { get; set; }
    public string? Sinopse { get; set; }
    public string? PaisOrigem { get; set; }
    public int? AnoLancamento { get; set; }
    public bool? EmExibicao { get; set; }
    public int? Plataforma { get; set; }
    public List<Guid>? GeneroIds { get; set; }
    public string? ImagemCapaUrl { get; set; }
}