using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Domain.Interfaces;

public interface IAvaliacaoRepository
{
    Task<Avaliacao?> ObterPorUsuarioETemporadaAsync(Guid usuarioId, Guid temporadaId);
    Task<bool> ExisteAvaliacaoAsync(Guid usuarioId, Guid temporadaId);
    Task AdicionarAsync(Avaliacao avaliacao);
    Task AtualizarAsync(Avaliacao avaliacao);
    Task RemoverAsync(Avaliacao avaliacao);
    Task<IEnumerable<Avaliacao>> ObterPorUsuarioAsync(Guid usuarioId);
    Task<IEnumerable<Avaliacao>> ObterTodasAsync();
}