using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Domain.Interfaces;

public interface IGeneroRepository
{
    Task<List<Genero>> ObterPorIdsAsync(List<Guid> ids);
    Task<Genero?> ObterPorIdAsync(Guid id);
}