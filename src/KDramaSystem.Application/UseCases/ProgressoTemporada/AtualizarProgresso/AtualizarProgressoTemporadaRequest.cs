namespace KDramaSystem.Application.UseCases.ProgressoTemporada.AtualizarProgresso;

public class AtualizarProgressoTemporadaRequest
{
    public Guid TemporadaId { get; set; }
    public int EpisodiosAssistidos { get; set; }
}