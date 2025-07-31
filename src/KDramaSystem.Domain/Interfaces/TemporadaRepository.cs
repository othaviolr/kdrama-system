using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Infrastructure.Repositories;

public class TemporadaRepository : ITemporadaRepository
{
    private readonly List<Temporada> _temporadas = new();

    public Task AdicionarAsync(Temporada temporada)
    {
        _temporadas.Add(temporada);
        return Task.CompletedTask;
    }

    public Task<IReadOnlyList<Temporada>> ObterPorDoramaIdAsync(Guid doramaId)
    {
        var temporadas = _temporadas
    .Where(t => t.DoramaId == doramaId)
    .OrderBy(t => t.Numero)
    .ToList();

        return Task.FromResult<IReadOnlyList<Temporada>>(temporadas);
    }

    public Task<Temporada?> ObterPorIdAsync(Guid id)
    {
        var temporada = _temporadas.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(temporada);
    }

    public Task AtualizarAsync(Temporada temporada)
    {
        var index = _temporadas.FindIndex(t => t.Id == temporada.Id);
        if (index != -1)
        {
            _temporadas[index] = temporada;
        }
        return Task.CompletedTask;
    }

    public Task ExcluirAsync(Guid id)
    {
        var temporada = _temporadas.FirstOrDefault(t => t.Id == id);
        if (temporada != null)
        {
            _temporadas.Remove(temporada);
        }
        return Task.CompletedTask;
    }
}