using KDramaSystem.Domain.ValueObjects;

namespace KDramaSystem.Application.UseCases.Atividade.Registrar;

public class RegistrarAtividadeRequest
{
    public Guid UsuarioId { get; set; }
    public TipoAtividade Tipo { get; set; } = null!;
    public Guid ReferenciaId { get; set; }
}