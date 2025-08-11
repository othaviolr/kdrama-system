using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Application.UseCases.ProgressoTemporada.AtualizarStatus;

public class AtualizarStatusTemporadaRequest
{
    public Guid TemporadaId { get; set; }
    public StatusDoramaEnum Status { get; set; }
}