namespace KDramaSystem.Application.UseCases.Temporada.Editar;

public class EditarTemporadaRequest
{
    public Guid Id { get; set; }
    public string? Nome { get; set; }
    public int AnoLancamento { get; set; }
    public bool EmExibicao { get; set; }
    public string? Sinopse { get; set; }
}