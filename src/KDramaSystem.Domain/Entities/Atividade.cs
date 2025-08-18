using KDramaSystem.Domain.ValueObjects;

namespace KDramaSystem.Domain.Entities;

public class Atividade
{
    public Guid Id { get; private set; }
    public Guid UsuarioId { get; private set; }
    public TipoAtividade Tipo { get; private set; }
    public Guid ReferenciaId { get; private set; }
    public DateTime Data { get; private set; }

    private Atividade() { }

    public Atividade(Guid usuarioId, TipoAtividade tipo, Guid referenciaId)
    {
        Id = Guid.NewGuid();
        UsuarioId = usuarioId;
        Tipo = tipo;
        ReferenciaId = referenciaId;
        Data = DateTime.UtcNow;
    }
}