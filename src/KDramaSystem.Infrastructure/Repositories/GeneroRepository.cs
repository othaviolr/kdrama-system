using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Infrastructure.Repositories;

public class GeneroRepository : IGeneroRepository
{
    private readonly List<Genero> _generos = new();

    public Task<List<Genero>> ObterPorIdsAsync(List<Guid> ids)
    {
        var generos = _generos.Where(g => ids.Contains(g.Id)).ToList();
        return Task.FromResult(generos);
    }

    public Task<Genero?> ObterPorIdAsync(Guid id)
    {
        var genero = _generos.FirstOrDefault(g => g.Id == id);
        return Task.FromResult(genero);
    }

    public Task AdicionarAsync(Genero genero)
    {
        _generos.Add(genero);
        return Task.CompletedTask;
    }
}