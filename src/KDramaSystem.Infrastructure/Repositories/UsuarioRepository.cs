using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly List<Usuario> _usuarios = new();

        public Task AdicionarAsync(Usuario usuario)
        {
            _usuarios.Add(usuario);
            return Task.CompletedTask;
        }

        public Task<bool> NomeUsuarioExisteAsync(string nomeUsuario)
        {
            return Task.FromResult(_usuarios.Any(u => u.NomeUsuario == nomeUsuario));
        }

        public Task<Usuario?> ObterPorIdAsync(Guid id)
        {
            return Task.FromResult(_usuarios.FirstOrDefault(u => u.Id == id));
        }

        public Task SalvarAsync(Usuario usuario)
        {
            return Task.CompletedTask;
        }

        public Task RemoverAsync(Guid id)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario != null)
            {
                _usuarios.Remove(usuario);
            }
            return Task.CompletedTask;
        }
    }
}