using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Domain.Interfaces.Repositories;

public interface IListaPrateleiraRepository
{
    Task AdicionarAsync(ListaPrateleira lista, CancellationToken cancellationToken = default);
    Task AtualizarAsync(ListaPrateleira lista, CancellationToken cancellationToken = default);
    Task RemoverAsync(Guid id, CancellationToken cancellationToken = default);

    Task<ListaPrateleira?> ObterPorIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<ListaPrateleira?> ObterPorTokenAsync(string shareToken, CancellationToken cancellationToken = default);
    Task<IEnumerable<ListaPrateleira>> ObterPorUsuarioAsync(Guid usuarioId, CancellationToken cancellationToken = default);
    Task<IEnumerable<ListaPrateleira>> ObterPublicasAsync(Guid usuarioIdIgnorado, CancellationToken cancellationToken = default);
    Task<IEnumerable<ListaPrateleira>> ObterMinhasAsync(Guid usuarioId, CancellationToken cancellationToken = default);
}