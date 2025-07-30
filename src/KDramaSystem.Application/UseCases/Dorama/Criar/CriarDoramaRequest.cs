namespace KDramaSystem.Application.UseCases.Dorama.Criar;

public class CriarDoramaRequest
{
    public Guid UsuarioCriadorId { get; init; }
    public string Titulo { get; init; } = default!;
    public string? TituloOriginal { get; init; }
    public string PaisOrigem { get; init; } = default!;
    public int AnoLancamento { get; init; }
    public bool EmExibicao { get; init; }
    public int Plataforma { get; init; }
    public List<Guid> GeneroIds { get; init; } = new();
    public string? Sinopse { get; init; }
    public List<Guid>? AtorIds { get; init; } = new();
    public string ImagemCapaUrl { get; init; } = default!;
}