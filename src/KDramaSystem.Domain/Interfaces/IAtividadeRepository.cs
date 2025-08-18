using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Domain.Interfaces;

public interface IAtividadeRepository
{
    Task RegistrarAsync(Atividade atividade);
    Task<IEnumerable<Atividade>> ObterPorUsuarioAsync(Guid usuarioId);
    Task<IEnumerable<Atividade>> ObterFeedAsync(Guid usuarioId, IEnumerable<Guid> seguindoIds);
}