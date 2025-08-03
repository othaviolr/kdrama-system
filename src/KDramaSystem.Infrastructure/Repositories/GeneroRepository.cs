using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Repositories;

public class GeneroRepository : IGeneroRepository
{
    private readonly KDramaDbContext _context;

    public GeneroRepository(KDramaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AdicionarAsync(Genero genero)
    {
        if (genero == null)
            throw new ArgumentNullException(nameof(genero));

        await _context.Generos.AddAsync(genero);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Genero genero)
    {
        if (genero == null)
            throw new ArgumentNullException(nameof(genero));

        _context.Generos.Update(genero);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Guid generoId)
    {
        if (generoId == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(generoId));

        var genero = await _context.Generos.FindAsync(generoId);
        if (genero != null)
        {
            _context.Generos.Remove(genero);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Genero?> ObterPorIdAsync(Guid generoId)
    {
        if (generoId == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(generoId));

        return await _context.Generos.FirstOrDefaultAsync(g => g.Id == generoId);
    }

    public async Task<List<Genero>> ListarAsync()
    {
        return await _context.Generos.ToListAsync();
    }

    public async Task<bool> ExisteComNomeAsync(string nome)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ArgumentException("Nome deve ser informado.", nameof(nome));

        return await _context.Generos
            .AnyAsync(g => g.Nome.ToLower() == nome.Trim().ToLower());
    }

    public async Task<List<Genero>> ObterPorIdsAsync(IEnumerable<Guid> ids)
    {
        if (ids == null || !ids.Any())
            return new List<Genero>();

        return await _context.Generos
            .Where(g => ids.Contains(g.Id))
            .ToListAsync();
    }
}