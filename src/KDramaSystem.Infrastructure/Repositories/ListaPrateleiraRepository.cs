using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Enums;
using KDramaSystem.Domain.Interfaces.Repositories;
using KDramaSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Repositories;

public class ListaPrateleiraRepository : IListaPrateleiraRepository
{
    private readonly KDramaDbContext _context;

    public ListaPrateleiraRepository(KDramaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(ListaPrateleira lista, CancellationToken cancellationToken = default)
    {
        await _context.ListasPrateleira.AddAsync(lista, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task AtualizarAsync(ListaPrateleira lista, CancellationToken cancellationToken = default)
    {
        _context.ListasPrateleira.Update(lista);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoverAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var lista = await _context.ListasPrateleira
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);

        if (lista != null)
        {
            _context.ListasPrateleira.Remove(lista);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<ListaPrateleira?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.ListasPrateleira
            .Include(l => l.Doramas)
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<ListaPrateleira?> ObterPorTokenAsync(string shareToken, CancellationToken cancellationToken = default)
    {
        return await _context.ListasPrateleira
            .Include(l => l.Doramas)
            .FirstOrDefaultAsync(l => l.ShareToken == shareToken
                                       && l.Privacidade == ListaPrivacidade.CompartilhadoLink,
                                       cancellationToken);
    }

    public async Task<IEnumerable<ListaPrateleira>> ObterPorUsuarioAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        return await _context.ListasPrateleira
            .Include(l => l.Doramas)
            .Where(l => l.UsuarioId == usuarioId)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ListaPrateleira>> ObterPublicasAsync(Guid usuarioIdIgnorado, CancellationToken cancellationToken = default)
    {
        return await _context.ListasPrateleira
            .Include(l => l.Doramas)
            .Where(l => l.Privacidade == ListaPrivacidade.Publico
                        && l.UsuarioId != usuarioIdIgnorado)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<ListaPrateleira>> ObterMinhasAsync(Guid usuarioId, CancellationToken cancellationToken = default)
    {
        return await _context.ListasPrateleira
            .Include(l => l.Doramas)
            .Where(l => l.UsuarioId == usuarioId)
            .ToListAsync(cancellationToken);
    }
}