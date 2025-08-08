using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Domain.Interfaces.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorIdAsync(Guid id);
        Task<Usuario?> ObterPorNomeUsuarioAsync(string nomeUsuario);
        Task<bool> NomeUsuarioExisteAsync(string nomeUsuario);
        Task AdicionarAsync(Usuario usuario);
        Task SalvarAsync(Usuario usuario);
        Task RemoverAsync(Guid id);
    }
}