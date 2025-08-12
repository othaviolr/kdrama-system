namespace KDramaSystem.Application.UseCases.Avaliacao.Criar;

public class CriarAvaliacaoRequest
{
    public Guid TemporadaId { get; set; }
    public int Nota { get; set; }
    public string? Comentario { get; set; }
    public Guid? RecomendadoPorUsuarioId { get; set; }
    public string? RecomendadoPorNomeLivre { get; set; }
}