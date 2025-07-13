using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Infrastructure.Repositories
{
    public class UsuarioAutenticacaoRepository : IUsuarioAutenticacaoRepository
    {
        private readonly List<UsuarioAutenticacao> _usuarios = new();

        public Task<bool> EmailExisteAsync(string email)
        {
            return Task.FromResult(_usuarios.Any(u => u.Email == email));
        }

        public Task<UsuarioAutenticacao?> ObterPorEmailAsync(string email)
        {
            return Task.FromResult(_usuarios.FirstOrDefault(u => u.Email == email));
        }

        public Task SalvarAsync(UsuarioAutenticacao usuarioAutenticacao)
        {
            _usuarios.Add(usuarioAutenticacao);
            return Task.CompletedTask;
        }
    }
}