using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Domain.Services;

public interface IBadgeService
{
    Task<List<Badge>> ConcederBadgesProgressaoAsync(Guid usuarioId);
}