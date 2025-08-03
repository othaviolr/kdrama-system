using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Domain.Interfaces;

public interface IAtorRepository
{
    Task AdicionarAsync(Ator ator);
    Task<Ator?> ObterPorIdAsync(Guid id);
    Task<List<Ator>> ObterTodosAsync();
    Task AtualizarAsync(Ator ator);
    Task ExcluirAsync(Guid id);
    Task<bool> ExisteComNomeAsync(string nome);
}