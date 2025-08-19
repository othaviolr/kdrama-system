using KDramaSystem.Domain.Enums;

namespace KDramaSystem.Application.DTOs.Atividade;

public class AtividadeDto
{
    public Guid UsuarioId { get; set; }
    public string UsuarioNome { get; set; } = string.Empty;
    public string UsuarioAvatarUrl { get; set; } = string.Empty;
    public TipoAtividadeEnum TipoAtividade { get; set; }
    public Guid? DoramaId { get; set; }
    public string? DoramaTitulo { get; set; }
    public int? TemporadaNumero { get; set; }
    public int? EpisodioNumero { get; set; }
    public decimal? Nota { get; set; }
    public string? Comentario { get; set; }
    public Guid? PrateleiraId { get; set; }
    public string? PrateleiraNome { get; set; }
    public DateTime CriadoEm { get; set; }
}