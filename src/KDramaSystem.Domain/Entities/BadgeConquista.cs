namespace KDramaSystem.Domain.Entities;

public class BadgeConquista
{
    public Guid Id { get; private set; }
    public Guid UsuarioId { get; private set; }
    public Guid BadgeId { get; private set; }
    public DateTime DataConquista { get; private set; }

    public BadgeConquista(Guid id, Guid usuarioId, Guid badgeId, DateTime dataConquista)
    {
        Id = id;
        UsuarioId = usuarioId;
        BadgeId = badgeId;
        DataConquista = dataConquista;
    }
}