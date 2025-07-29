using KDramaSystem.Application.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.Usuario.DeixarDeSeguir;

public class DeixarDeSeguirUsuarioUseCase
{
    private readonly IUsuarioRelacionamentoRepository _relacionamentoRepository;

    public DeixarDeSeguirUsuarioUseCase(IUsuarioRelacionamentoRepository relacionamentoRepository)
    {
        _relacionamentoRepository = relacionamentoRepository;
    }

    public async Task<bool> ExecutarAsync(Guid usuarioLogadoId, DeixarDeSeguirUsuarioRequest request)
    {
        await _relacionamentoRepository.RemoverAsync(usuarioLogadoId, request.UsuarioAlvoId);
        return true;
    }
}