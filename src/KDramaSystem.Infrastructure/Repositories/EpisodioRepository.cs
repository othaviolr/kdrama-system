using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Repositories;

public class EpisodioRepository : IEpisodioRepository
{
    private readonly KDramaDbContext _context;

    public EpisodioRepository(KDramaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AdicionarAsync(Episodio episodio)
    {
        if (episodio == null)
            throw new ArgumentNullException(nameof(episodio));

        await _context.Episodios.AddAsync(episodio);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Episodio episodio)
    {
        if (episodio == null)
            throw new ArgumentNullException(nameof(episodio));

        _context.Episodios.Update(episodio);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Guid episodioId)
    {
        if (episodioId == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(episodioId));

        var episodio = await _context.Episodios.FindAsync(episodioId);
        if (episodio != null)
        {
            _context.Episodios.Remove(episodio);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Episodio?> ObterPorIdAsync(Guid episodioId)
    {
        if (episodioId == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(episodioId));

        return await _context.Episodios.FirstOrDefaultAsync(e => e.Id == episodioId);
    }

    public async Task<List<Episodio>> ListarPorTemporadaAsync(Guid temporadaId)
    {
        if (temporadaId == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(temporadaId));

        return await _context.Episodios
            .Where(e => e.TemporadaId == temporadaId)
            .OrderBy(e => e.Numero)
            .ToListAsync();
    }
}