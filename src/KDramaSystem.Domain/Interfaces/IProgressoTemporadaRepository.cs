using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Domain.Interfaces;

public interface IProgressoTemporadaRepository
{
    Task<ProgressoTemporada?> ObterPorUsuarioETemporadaAsync(Guid usuarioId, Guid temporadaId);
    Task CriarAsync(ProgressoTemporada progressoTemporada);
    Task AtualizarAsync(ProgressoTemporada progressoTemporada);
    Task ExcluirAsync (Guid progressoId);
}