using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Domain.Interfaces.Repositories;

public interface IDoramaListaRepository
{
    Task AdicionarAsync(DoramaLista doramaLista);
    Task RemoverAsync(Guid listaPrateleiraId, Guid doramaId);
    Task<List<DoramaLista>> ObterPorListaIdAsync(Guid listaPrateleiraId);
}