using KDramaSystem.Application.Interfaces;
using KDramaSystem.Application.UseCases.Usuario.Dtos;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.Usuario.ObterPerfilCompleto
{
    public class ObterPerfilCompletoUseCase : IObterPerfilCompletoUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioAutenticadoProvider _usuarioAutenticado;

        public ObterPerfilCompletoUseCase(
            IUsuarioRepository usuarioRepository,
            IUsuarioAutenticadoProvider usuarioAutenticado)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioAutenticado = usuarioAutenticado;
        }

        public async Task<PerfilCompletoDto?> ExecutarAsync(ObterPerfilCompletoRequest request)
        {
            var usuarioId = _usuarioAutenticado.ObterUsuarioId();
            var usuario = await _usuarioRepository.ObterPorIdAsync(usuarioId);

            if (usuario == null)
                return null;

            return new PerfilCompletoDto
            {
                Nome = usuario.Nome,
                NomeUsuario = usuario.NomeUsuario,
                Email = usuario.Email,
                FotoUrl = usuario.FotoUrl,
                Bio = usuario.Bio
            };
        }
    }
}