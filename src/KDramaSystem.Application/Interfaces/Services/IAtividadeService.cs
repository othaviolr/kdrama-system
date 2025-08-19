using KDramaSystem.Application.DTOs.Atividade;

namespace KDramaSystem.Application.Interfaces.Services;

public interface IAtividadeService
{
    Task<List<AtividadeDto>> ObterFeedAsync(int quantidade);
    Task<List<AtividadeDto>> ObterAtividadesUsuarioAsync(Guid usuarioId);
}