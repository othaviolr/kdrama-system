namespace KDramaSystem.Application.DTOs.Atividade;

public class ObterAtividadeDto
{
    public Guid Id { get; set; }
    public string Tipo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime Data { get; set; }
}