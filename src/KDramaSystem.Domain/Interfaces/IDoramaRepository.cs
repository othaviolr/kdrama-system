using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Domain.Interfaces;

public interface IDoramaRepository
{
    Task AdicionarAsync(Dorama dorama);
    Task<Dorama?> ObterPorIdAsync(Guid id);
    Task<bool> ExisteComTituloAsync(string titulo);
    Task AtualizarAsync(Dorama dorama);
    Task ExcluirAsync(Guid id);
}