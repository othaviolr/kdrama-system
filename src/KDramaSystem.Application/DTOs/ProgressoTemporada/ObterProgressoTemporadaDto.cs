using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Application.DTOs.ProgressoTemporada;

public class ObterProgressoTemporadaDto
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }
    public Guid TemporadaId { get; set; }
    public int EpisodiosAssistidos { get; set; }
    public StatusDoramaEnum Status { get; set; }
    public DateTime DataAtualizacao { get; set; }
}