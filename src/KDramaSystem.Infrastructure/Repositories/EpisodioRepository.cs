using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Infrastructure.Repositories
{
    public class EpisodioRepository : IEpisodioRepository
    {
        private readonly List<Episodio> _episodios = new();

        public Task AdicionarAsync(Episodio episodio)
        {
            _episodios.Add(episodio);
            return Task.CompletedTask;
        }

        public Task AtualizarAsync(Episodio episodio)
        {
            var index = _episodios.FindIndex(e => e.Id == episodio.Id);
            if (index != -1)
            {
                _episodios[index] = episodio;
            }

            return Task.CompletedTask;
        }

        public Task RemoverAsync(Guid episodioId)
        {
            var episodio = _episodios.FirstOrDefault(e => e.Id == episodioId);
            if (episodio != null)
            {
                _episodios.Remove(episodio);
            }

            return Task.CompletedTask;
        }

        public Task<Episodio?> ObterPorIdAsync(Guid episodioId)
        {
            var episodio = _episodios.FirstOrDefault(e => e.Id == episodioId);
            return Task.FromResult(episodio);
        }

        public Task<List<Episodio>> ListarPorTemporadaAsync(Guid temporadaId)
        {
            var lista = _episodios
                .Where(e => e.TemporadaId == temporadaId)
                .OrderBy(e => e.Numero)
                .ToList();

            return Task.FromResult(lista);
        }
    }
}