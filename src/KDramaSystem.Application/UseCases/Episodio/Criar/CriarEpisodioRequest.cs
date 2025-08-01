using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Application.UseCases.Episodio.Criar;

public class CriarEpisodioRequest
{
    public Guid TemporadaId { get; set; }
    public int Numero { get; set; }
    public string Titulo { get; set; } = null!;
    public int DuracaoMinutos { get; set; }
    public TipoEpisodio Tipo { get; set; } = TipoEpisodio.Regular;
    public string? Sinopse { get; set; }
}