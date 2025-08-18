using KDramaSystem.Domain.ValueObjects;

namespace KDramaSystem.Application.DTOs.Atividade;

public class RegistrarAtividadeDto
{
    public Guid UsuarioId { get; set; }
    public TipoAtividade Tipo { get; set; } = null!;
    public Guid ReferenciaId { get; set; }
}