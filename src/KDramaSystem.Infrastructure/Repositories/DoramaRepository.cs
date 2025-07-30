using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Infrastructure.Repositories;

public class DoramaRepository : IDoramaRepository
{
    private readonly List<Dorama> _doramas = new();

    public Task AdicionarAsync(Dorama dorama)
    {
        _doramas.Add(dorama);
        return Task.CompletedTask;
    }

    public Task<Dorama?> ObterPorIdAsync(Guid id)
    {
        var dorama = _doramas.FirstOrDefault(d => d.Id == id);
        return Task.FromResult(dorama);
    }

    public Task<bool> ExisteComTituloAsync(string titulo)
    {
        var exists = _doramas.Any(d => d.Titulo == titulo);
        return Task.FromResult(exists);
    }

    public Task AtualizarAsync(Dorama dorama)
    {
        var index = _doramas.FindIndex(d => d.Id == dorama.Id);
        if (index != -1)
        {
            _doramas[index] = dorama;
        }

        return Task.CompletedTask;
    }
}