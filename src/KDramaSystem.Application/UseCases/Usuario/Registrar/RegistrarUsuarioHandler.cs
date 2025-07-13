using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces.Repositories;
using KDramaSystem.Domain.Interfaces.Services;

namespace KDramaSystem.Application.UseCases.Usuario.Registrar
{
    public class RegistrarUsuarioHandler
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioAutenticacaoRepository _authRepository;
        private readonly ICriptografiaService _criptografiaService;
        private readonly ITokenService _tokenService;

        public RegistrarUsuarioHandler(
            IUsuarioRepository usuarioRepository,
            IUsuarioAutenticacaoRepository authRepository,
            ICriptografiaService criptografiaService,
            ITokenService tokenService)
        {
            _usuarioRepository = usuarioRepository;
            _authRepository = authRepository;
            _criptografiaService = criptografiaService;
            _tokenService = tokenService;
        }

        public async Task<RegistrarUsuarioResult> Handle(RegistrarUsuarioCommand command)
        {
            var emailExiste = await _authRepository.EmailExisteAsync(command.Email);
            var nomeUsuarioExiste = await _usuarioRepository.NomeUsuarioExisteAsync(command.NomeUsuario);

            if (emailExiste) throw new InvalidOperationException("E-mail já está em uso.");
            if (nomeUsuarioExiste) throw new InvalidOperationException("Nome de usuário já está em uso.");

            var id = Guid.NewGuid();
            var senhaHash = _criptografiaService.GerarHash(command.Senha);

            var usuario = new KDramaSystem.Domain.Entities.Usuario(id, command.Nome, command.NomeUsuario, command.Email);
            var auth = new UsuarioAutenticacao(id, command.Email, senhaHash);

            await _usuarioRepository.AdicionarAsync(usuario);
            await _authRepository.SalvarAsync(auth);

            var token = _tokenService.GerarToken(usuario.Id, usuario.NomeUsuario, usuario.Email);

            return new RegistrarUsuarioResult(usuario.Id, usuario.Nome, usuario.NomeUsuario, usuario.Email, token);
        }
    }
}