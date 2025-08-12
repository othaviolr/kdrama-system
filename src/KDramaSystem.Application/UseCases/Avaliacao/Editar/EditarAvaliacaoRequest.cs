namespace KDramaSystem.Application.UseCases.Avaliacao.Editar;

public class EditarAvaliacaoRequest
{
    public Guid TemporadaId { get; set; }
    public int Nota { get; set; }
    public string? Comentario { get; set; }
    public Guid? RecomendadoPorUsuarioId { get; set; }
    public string? RecomendadoPorNomeLivre { get; set; }
}