using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Application.Interfaces.Repositories;

public interface IUsuarioRelacionamentoRepository
{
    Task<bool> ExisteRelacionamentoAsync(Guid seguidorId, Guid seguindoId);
    Task CriarAsync(UsuarioRelacionamento relacionamento);
    Task RemoverAsync(Guid seguidorId, Guid seguindoId);
    Task<List<Usuario>> ObterSeguidoresAsync(Guid usuarioId);
    Task<List<Usuario>> ObterSeguindoAsync(Guid usuarioId);
}