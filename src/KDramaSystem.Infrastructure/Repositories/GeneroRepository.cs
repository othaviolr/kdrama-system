using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Infrastructure.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly List<Genero> _generos = new();

        public Task AdicionarAsync(Genero genero)
        {
            _generos.Add(genero);
            return Task.CompletedTask;
        }

        public Task AtualizarAsync(Genero genero)
        {
            var index = _generos.FindIndex(g => g.Id == genero.Id);
            if (index != -1)
            {
                _generos[index] = genero;
            }

            return Task.CompletedTask;
        }

        public Task RemoverAsync(Guid generoId)
        {
            var genero = _generos.FirstOrDefault(g => g.Id == generoId);
            if (genero != null)
            {
                _generos.Remove(genero);
            }

            return Task.CompletedTask;
        }

        public Task<Genero?> ObterPorIdAsync(Guid generoId)
        {
            var genero = _generos.FirstOrDefault(g => g.Id == generoId);
            return Task.FromResult(genero);
        }

        public Task<List<Genero>> ListarAsync()
        {
            return Task.FromResult(_generos.ToList());
        }

        public Task<bool> ExisteComNomeAsync(string nome)
        {
            var exists = _generos.Any(g => g.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
            return Task.FromResult(exists);
        }

        public Task<List<Genero>> ObterPorIdsAsync(IEnumerable<Guid> ids)
        {
            var lista = _generos.Where(g => ids.Contains(g.Id)).ToList();
            return Task.FromResult(lista);
        }
    }
}