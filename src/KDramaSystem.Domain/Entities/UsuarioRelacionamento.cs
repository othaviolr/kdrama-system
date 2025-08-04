using KDramaSystem.Domain.Entities;

public class UsuarioRelacionamento
{
    public Guid Id { get; private set; }
    public Guid SeguidorId { get; private set; }
    public Guid SeguindoId { get; private set; }
    public DateTime Data { get; private set; }

    public Usuario Seguidor { get; private set; }
    public Usuario Seguindo { get; private set; }

    private UsuarioRelacionamento() { }

    public UsuarioRelacionamento(Guid seguidorId, Guid seguindoId)
    {
        if (seguidorId == seguindoId)
            throw new ArgumentException("Um usuário não pode seguir a si mesmo.");

        Id = Guid.NewGuid();
        SeguidorId = seguidorId;
        SeguindoId = seguindoId;
        Data = DateTime.UtcNow;
    }
}