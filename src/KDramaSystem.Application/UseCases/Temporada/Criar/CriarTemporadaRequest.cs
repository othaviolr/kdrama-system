namespace KDramaSystem.Application.UseCases.Temporada.Criar;

public class CriarTemporadaRequest
{
    public Guid DoramaId { get; set; }
    public int Numero { get; set; }
    public int AnoLancamento { get; set; }
    public bool EmExibicao { get; set; }
    public string? Nome { get; set; }
    public string? Sinopse { get; set; }
}