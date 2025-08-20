using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Repositories;

public class DoramaRepository : IDoramaRepository
{
    private readonly KDramaDbContext _context;

    public DoramaRepository(KDramaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AdicionarAsync(Dorama dorama)
    {
        if (dorama == null)
            throw new ArgumentNullException(nameof(dorama));

        await _context.Doramas.AddAsync(dorama);
        await _context.SaveChangesAsync();
    }

    public async Task<Dorama?> ObterPorIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(id));

        return await _context.Doramas
            .Include(d => d.Generos)
            .Include(d => d.Temporadas)
            .Include(d => d.Atores)
            .ThenInclude(da => da.Ator)
            .FirstOrDefaultAsync(d => d.Id == id);
    }

    public async Task<bool> ExisteComTituloAsync(string titulo)
    {
        if (string.IsNullOrWhiteSpace(titulo))
            throw new ArgumentException("Título deve ser informado.", nameof(titulo));

        return await _context.Doramas.AnyAsync(d => d.Titulo == titulo);
    }

    public async Task AtualizarAsync(Dorama dorama)
    {
        if (dorama == null)
            throw new ArgumentNullException(nameof(dorama));

        _context.Doramas.Update(dorama);
        await _context.SaveChangesAsync();
    }

    public async Task ExcluirAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(id));

        var dorama = await _context.Doramas.FindAsync(id);
        if (dorama != null)
        {
            _context.Doramas.Remove(dorama);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<List<Dorama>> ObterTodosAsync()
    {
        return await _context.Doramas
            .Include(d => d.Generos)
            .Include(d => d.Temporadas)
            .Include(d => d.Atores)
                .ThenInclude(da => da.Ator)
            .ToListAsync();
    }
}