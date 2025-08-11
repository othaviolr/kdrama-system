using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Domain.Interfaces;

public interface ITemporadaRepository
{
    Task<Temporada?> ObterPorIdAsync(Guid id);
    Task<IReadOnlyList<Temporada>> ObterPorDoramaIdAsync(Guid doramaId);
    Task AdicionarAsync(Temporada temporada);
    Task AtualizarAsync(Temporada temporada);
    Task ExcluirAsync(Guid id);
    Task<int> ContarEpisodiosAsync(Guid temporadaId);
}