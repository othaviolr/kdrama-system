namespace KDramaSystem.Application.DTOs.Avaliacao;

public class ObterAvaliacaoDto
{
    public Guid Id { get; set; }
    public Guid UsuarioId { get; set; }        
    public Guid TemporadaId { get; set; }
    public int Nota { get; set; }                 
    public string? Comentario { get; set; }       
    public Guid? RecomendadoPorUsuarioId { get; set; }
    public string? RecomendadoPorNomeLivre { get; set; }
    public DateTime DataAvaliacao { get; set; }
}