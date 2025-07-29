using KDramaSystem.Application.Interfaces.Repositories;
using KDramaSystem.Application.UseCases.Usuario.Seguir;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces.Repositories;

namespace KDramaSystem.Application.UseCases.Usuario
{
    public class SeguirUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioRelacionamentoRepository _relacionamentoRepository;

        public SeguirUsuarioUseCase(
            IUsuarioRepository usuarioRepository,
            IUsuarioRelacionamentoRepository relacionamentoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _relacionamentoRepository = relacionamentoRepository;
        }

        public async Task<bool> ExecutarAsync(Guid usuarioLogadoId, SeguirUsuarioRequest request)
        {
            if (usuarioLogadoId == request.UsuarioAlvoId)
                throw new InvalidOperationException("Você não pode seguir a si mesmo.");

            var usuarioAlvo = await _usuarioRepository.ObterPorIdAsync(request.UsuarioAlvoId);
            if (usuarioAlvo is null)
                throw new KeyNotFoundException("Usuário a ser seguido não encontrado.");

            bool jaSegue = await _relacionamentoRepository
                .ExisteRelacionamentoAsync(usuarioLogadoId, request.UsuarioAlvoId);

            if (jaSegue)
                throw new InvalidOperationException("Você já segue esse usuário.");

            var relacionamento = new UsuarioRelacionamento(usuarioLogadoId, request.UsuarioAlvoId);

            await _relacionamentoRepository.CriarAsync(relacionamento);

            return true;
        }
    }
}
