using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Repositories;

public class BadgeUsuarioRepository : IBadgeUsuarioRepository
{
    private readonly KDramaDbContext _context;

    public BadgeUsuarioRepository(KDramaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<List<BadgeConquista>> ObterPorUsuarioAsync(Guid usuarioId)
    {
        if (usuarioId == Guid.Empty)
            throw new ArgumentException("Id do usuário inválido.", nameof(usuarioId));

        return await _context.BadgesConquistadas
            .Where(b => b.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task AdicionarAsync(Guid usuarioId, Guid badgeId)
    {
        if (usuarioId == Guid.Empty)
            throw new ArgumentException("Id do usuário inválido.", nameof(usuarioId));
        if (badgeId == Guid.Empty)
            throw new ArgumentException("Id da badge inválido.", nameof(badgeId));

        var conquista = new BadgeConquista(
            Guid.NewGuid(),
            usuarioId,
            badgeId,
            DateTime.UtcNow
        );

        await _context.BadgesConquistadas.AddAsync(conquista);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> UsuarioPossuiBadgeAsync(Guid usuarioId, Guid badgeId)
    {
        if (usuarioId == Guid.Empty || badgeId == Guid.Empty)
            return false;

        return await _context.BadgesConquistadas
            .AnyAsync(b => b.UsuarioId == usuarioId && b.BadgeId == badgeId);
    }
}