using KDramaSystem.Application.Interfaces;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.Usuario.Editar
{
    public class EditarPerfilUseCase : IEditarPerfilUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioAutenticadoProvider _usuarioAutenticado;

        public EditarPerfilUseCase(
            IUsuarioRepository usuarioRepository,
            IUsuarioAutenticadoProvider usuarioAutenticado)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioAutenticado = usuarioAutenticado;
        }

        public async Task ExecutarAsync(EditarPerfilRequest request)
        {
            var usuarioId = _usuarioAutenticado.ObterUsuarioId();
            var usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);

            if (usuario is null)
                throw new Exception("Usuário não encontrado.");

            usuario.EditarPerfil(request.Nome, request.NomeUsuario, request.FotoUrl, request.Bio);
            await _usuarioRepository.SalvarAsync(usuario);
        }
    }
}