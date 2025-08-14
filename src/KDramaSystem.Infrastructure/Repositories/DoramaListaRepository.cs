using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces.Repositories;
using KDramaSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Repositories;

public class DoramaListaRepository : IDoramaListaRepository
{
    private readonly KDramaDbContext _context;

    public DoramaListaRepository(KDramaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(DoramaLista doramaLista)
    {
        await _context.DoramasLista.AddAsync(doramaLista);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Guid listaPrateleiraId, Guid doramaId)
    {
        var item = await _context.DoramasLista
            .FirstOrDefaultAsync(d => d.ListaPrateleiraId == listaPrateleiraId && d.DoramaId == doramaId);

        if (item != null)
        {
            _context.DoramasLista.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<DoramaLista>> ObterPorListaIdAsync(Guid listaPrateleiraId)
    {
        return await _context.DoramasLista
            .Where(d => d.ListaPrateleiraId == listaPrateleiraId)
            .ToListAsync();
    }
}