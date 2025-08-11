using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Application.DTOs.ProgressoTemporada;

public class AtualizarStatusTemporadaDto
{
    public Guid TemporadaId { get; set; }
    public StatusDoramaEnum Status { get; set; }
}