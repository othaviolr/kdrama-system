using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Domain.Interfaces.Repositories
{
    public interface IUsuarioAutenticacaoRepository
    {
        Task<UsuarioAutenticacao?> ObterPorEmailAsync(string email);
        Task<bool> EmailExisteAsync(string email);
        Task SalvarAsync(UsuarioAutenticacao usuarioAutenticacao);
    }
}