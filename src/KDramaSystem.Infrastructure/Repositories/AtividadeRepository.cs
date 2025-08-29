using KDramaSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using KDramaSystem.Infrastructure.Persistence;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Infrastructure.Repositories;

public class AtividadeRepository : IAtividadeRepository
{
    private readonly KDramaDbContext _context;

    public AtividadeRepository(KDramaDbContext context)
    {
        _context = context;
    }

    public async Task RegistrarAsync(Atividade atividade)
    {
        await _context.Atividades.AddAsync(atividade);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Atividade>> ObterPorUsuarioAsync(Guid usuarioId)
    {
        return await _context.Atividades
            .Where(a => a.UsuarioId == usuarioId)
            .OrderByDescending(a => a.Data)
            .ToListAsync();
    }

    public async Task<IEnumerable<Atividade>> ObterFeedAsync(Guid usuarioId, IEnumerable<Guid> seguindoIds)
    {
        var usuariosIds = seguindoIds.Append(usuarioId);
        return await _context.Atividades
            .Where(a => usuariosIds.Contains(a.UsuarioId))
            .Include(a => a.Usuario)
            .OrderByDescending(a => a.Data)
            .ToListAsync();
    }
}