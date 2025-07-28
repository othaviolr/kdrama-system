using KDramaSystem.Application.Interfaces;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.Usuario.Deletar;

public class DeletarPerfilUseCase : IDeletarPerfilUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IUsuarioAutenticadoProvider _usuarioAutenticadoProvider;

    public DeletarPerfilUseCase(IUsuarioRepository usuarioRepository, IUsuarioAutenticadoProvider usuarioAutenticadoProvider)
    {
        _usuarioRepository = usuarioRepository;
        _usuarioAutenticadoProvider = usuarioAutenticadoProvider;
    }

    public async Task ExecutarAsync()
    {
        var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();
        var usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);

        if (usuario is null)
            throw new InvalidOperationException("Usuário não encontrado.");
        
        await _usuarioRepository.RemoverAsync(usuario.Id);
    }
}