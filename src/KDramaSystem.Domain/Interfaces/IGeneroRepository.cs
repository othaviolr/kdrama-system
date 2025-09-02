using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Domain.Interfaces
{
    public interface IGeneroRepository
    {
        Task AdicionarAsync(Genero genero);
        Task AtualizarAsync(Genero genero);
        Task RemoverAsync(Guid generoId);
        Task<Genero?> ObterPorIdAsync(Guid generoId);
        Task<List<Genero>> ListarAsync();
        Task<bool> ExisteComNomeAsync(string nome);
        Task<List<Genero>> ObterPorIdsAsync(IEnumerable<Guid> ids);
        Task<List<Genero>> ObterTodosAsync();
    }
}