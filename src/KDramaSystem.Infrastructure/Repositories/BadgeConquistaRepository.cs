using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Repositories
{
    public class BadgeConquistaRepository : IBadgeConquistaRepository
    {
        private readonly KDramaDbContext _context;

        public BadgeConquistaRepository(KDramaDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<List<BadgeConquista>> ObterPorUsuarioAsync(Guid usuarioId)
        {
            return await _context.BadgesConquistadas
                .Where(bc => bc.UsuarioId == usuarioId)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Guid usuarioId, Guid badgeId)
        {
            var conquista = new BadgeConquista
            (
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
            return await _context.BadgesConquistadas
                .AnyAsync(bc => bc.UsuarioId == usuarioId && bc.BadgeId == badgeId);
        }
    }
}