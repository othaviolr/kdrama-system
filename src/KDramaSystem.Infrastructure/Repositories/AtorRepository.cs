using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Repositories;

public class AtorRepository : IAtorRepository
{
    private readonly KDramaDbContext _context;

    public AtorRepository(KDramaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AdicionarAsync(Ator ator)
    {
        if (ator == null)
            throw new ArgumentNullException(nameof(ator));

        await _context.Atores.AddAsync(ator);
        await _context.SaveChangesAsync();
    }

    public async Task<Ator?> ObterPorIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(id));

        return await _context.Atores
            .Include(a => a.Doramas)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Ator>> ObterTodosAsync()
    {
        return await _context.Atores.ToListAsync();
    }

    public async Task AtualizarAsync(Ator ator)
    {
        if (ator == null)
            throw new ArgumentNullException(nameof(ator));

        _context.Atores.Update(ator);
        await _context.SaveChangesAsync();
    }

    public async Task ExcluirAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(id));

        var ator = await _context.Atores.FindAsync(id);
        if (ator != null)
        {
            _context.Atores.Remove(ator);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> ExisteComNomeAsync(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome deve ser informado.", nameof(nome));

        return await _context.Atores
            .AnyAsync(a => a.Nome.ToLower() == nome.Trim().ToLower());
    }

    public async Task<List<Ator>> ObterPorIdsAsync(IEnumerable<Guid> ids)
    {
        if (ids == null || !ids.Any())
            return new List<Ator>();

        return await _context.Atores
            .Where(a => ids.Contains(a.Id))
            .ToListAsync();
    }

    public async Task<Ator?> ObterPorNomeAsync(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome deve ser informado.", nameof(nome));

        return await _context.Atores
            .Include(a => a.Doramas)
            .FirstOrDefaultAsync(a => a.Nome.ToLower() == nome.Trim().ToLower());
    }
}