using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Domain.Interfaces;

public interface IEpisodioRepository
{
    Task AdicionarAsync(Episodio episodio);
    Task AtualizarAsync(Episodio episodio);
    Task RemoverAsync(Guid episodioId);
    Task<Episodio?> ObterPorIdAsync(Guid episodioId);
    Task<List<Episodio>> ListarPorTemporadaAsync(Guid temporadaId);
}