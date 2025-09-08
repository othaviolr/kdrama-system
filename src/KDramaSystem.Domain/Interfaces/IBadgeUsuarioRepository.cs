using KDramaSystem.Domain.Entities;

public interface IBadgeUsuarioRepository
{
    Task<List<BadgeConquista>> ObterPorUsuarioAsync(Guid usuarioId);
    Task AdicionarAsync(Guid usuarioId, Guid badgeId);
    Task<bool> UsuarioPossuiBadgeAsync(Guid usuarioId, Guid badgeId);
}