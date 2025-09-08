using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Domain.Interfaces;

public interface IBadgeConquistaRepository
{
    Task<List<BadgeConquista>> ObterPorUsuarioAsync(Guid usuarioId);
    Task AdicionarAsync(Guid usuarioId, Guid badgeId);
    Task<bool> UsuarioPossuiBadgeAsync(Guid usuarioId, Guid badgeId);
}