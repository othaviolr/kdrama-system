using KDramaSystem.Domain.Interfaces.Repositories;
using KDramaSystem.Domain.Interfaces.Services;

namespace KDramaSystem.Application.UseCases.Usuario.Login
{
    public class LoginUsuarioHandler
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioAutenticacaoRepository _authRepository;
        private readonly ICriptografiaService _criptografiaService;
        private readonly ITokenService _tokenService;

        public LoginUsuarioHandler(
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

        public async Task<LoginUsuarioResult> Handle(LoginUsuarioCommand command)
        {
            var auth = await _authRepository.ObterPorEmailAsync(command.Email)
                ?? throw new InvalidOperationException("E-mail ou senha inválidos.");

            var senhaValida = _criptografiaService.VerificarSenha(command.Senha, auth.SenhaHash);
            if (!senhaValida)
                throw new InvalidOperationException("E-mail ou senha inválidos.");

            var usuario = await _usuarioRepository.ObterPorIdAsync(auth.UsuarioId)
                ?? throw new InvalidOperationException("Usuário não encontrado.");

            var token = _tokenService.GerarToken(usuario.Id, usuario.NomeUsuario, usuario.Email);

            return new LoginUsuarioResult(usuario.Id, usuario.Nome, usuario.NomeUsuario, usuario.Email, token);
        }
    }
}