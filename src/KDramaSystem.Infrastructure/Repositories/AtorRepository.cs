using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Infrastructure.Repositories;

public class AtorRepository : IAtorRepository
{
    private readonly List<Ator> _atores = new();

    public Task AdicionarAsync(Ator ator)
    {
        _atores.Add(ator);
        return Task.CompletedTask;
    }

    public Task<Ator?> ObterPorIdAsync(Guid id)
    {
        var ator = _atores.FirstOrDefault(a => a.Id == id);
        return Task.FromResult(ator);
    }

    public Task<List<Ator>> ObterTodosAsync()
    {
        return Task.FromResult(_atores.ToList());
    }

    public Task AtualizarAsync(Ator ator)
    {
        var index = _atores.FindIndex(a => a.Id == ator.Id);
        if (index != -1)
        {
            _atores[index] = ator;
        }

        return Task.CompletedTask;
    }

    public Task ExcluirAsync(Guid id)
    {
        var ator = _atores.FirstOrDefault(a => a.Id == id);
        if (ator != null)
        {
            _atores.Remove(ator);
        }

        return Task.CompletedTask;
    }

    public Task<bool> ExisteComNomeAsync(string nome)
    {
        var exists = _atores.Any(a => a.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
        return Task.FromResult(exists);
    }
}