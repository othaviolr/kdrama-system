using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Repositories;

public class TemporadaRepository : ITemporadaRepository
{
    private readonly KDramaDbContext _context;

    public TemporadaRepository(KDramaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AdicionarAsync(Temporada temporada)
    {
        if (temporada == null)
            throw new ArgumentNullException(nameof(temporada));

        await _context.Temporadas.AddAsync(temporada);
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<Temporada>> ObterPorDoramaIdAsync(Guid doramaId)
    {
        if (doramaId == Guid.Empty)
            throw new ArgumentException("Id do dorama inválido.", nameof(doramaId));

        return await _context.Temporadas
            .Include(t => t.Episodios)
            .Where(t => t.DoramaId == doramaId)
            .OrderBy(t => t.Numero)
            .ToListAsync();
    }

    public async Task<Temporada?> ObterPorIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(id));

        return await _context.Temporadas
            .Include(t => t.Episodios)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AtualizarAsync(Temporada temporada)
    {
        if (temporada == null)
            throw new ArgumentNullException(nameof(temporada));

        _context.Temporadas.Update(temporada);
        await _context.SaveChangesAsync();
    }

    public async Task ExcluirAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(id));

        var temporada = await _context.Temporadas.FindAsync(id);
        if (temporada != null)
        {
            _context.Temporadas.Remove(temporada);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> ContarEpisodiosAsync(Guid temporadaId)
    {
        return await _context.Episodios.CountAsync(e => e.TemporadaId == temporadaId);
    }
}