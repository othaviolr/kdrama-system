using KDramaSystem.Application.UseCases.Usuario.Login;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces.Repositories;
using KDramaSystem.Domain.Interfaces.Services;
using Moq;

namespace KDramaSystem.Tests
{
    public class LoginUsuarioHandlerTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock = new();
        private readonly Mock<IUsuarioAutenticacaoRepository> _authRepositoryMock = new();
        private readonly Mock<ICriptografiaService> _criptografiaServiceMock = new();
        private readonly Mock<ITokenService> _tokenServiceMock = new();

        [Fact]
        public async Task Handle_ComandoValido_DeveRetornarLoginResultComToken()
        {
            var email = "email@teste.com";
            var senha = "senha123";
            var senhaHash = "hash_da_senha";
            var usuarioId = Guid.NewGuid();

            var command = new LoginUsuarioCommand(email, senha);

            var authEntity = new UsuarioAutenticacao(usuarioId, email, senhaHash);
            var usuarioEntity = new KDramaSystem.Domain.Entities.Usuario(usuarioId, "Nome Teste", "nomeusuario", email);

            _authRepositoryMock.Setup(x => x.ObterPorEmailAsync(email)).ReturnsAsync(authEntity);
            _criptografiaServiceMock.Setup(x => x.VerificarSenha(senha, senhaHash)).Returns(true);
            _usuarioRepositoryMock.Setup(x => x.ObterPorIdAsync(usuarioId)).ReturnsAsync(usuarioEntity);
            _tokenServiceMock.Setup(x => x.GerarToken(usuarioId, usuarioEntity.NomeUsuario, email)).Returns("token123");

            var handler = new LoginUsuarioHandler(
                _usuarioRepositoryMock.Object,
                _authRepositoryMock.Object,
                _criptografiaServiceMock.Object,
                _tokenServiceMock.Object
            );

            var result = await handler.Handle(command);

            Assert.NotNull(result);
            Assert.Equal(usuarioId, result.UsuarioId);
            Assert.Equal(usuarioEntity.Nome, result.Nome);
            Assert.Equal(usuarioEntity.NomeUsuario, result.NomeUsuario);
            Assert.Equal(email, result.Email);
            Assert.Equal("token123", result.Token);
        }

        [Fact]
        public async Task Handle_EmailInvalido_DeveLancarInvalidOperationException()
        {
            var command = new LoginUsuarioCommand("email@teste.com", "senha123");
            _authRepositoryMock.Setup(x => x.ObterPorEmailAsync(command.Email)).ReturnsAsync((UsuarioAutenticacao)null);

            var handler = new LoginUsuarioHandler(
                _usuarioRepositoryMock.Object,
                _authRepositoryMock.Object,
                _criptografiaServiceMock.Object,
                _tokenServiceMock.Object
            );

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command));
            Assert.Equal("E-mail ou senha inválidos.", exception.Message);
        }

        [Fact]
        public async Task Handle_SenhaInvalida_DeveLancarInvalidOperationException()
        {
            var email = "email@teste.com";
            var senhaHash = "hash_da_senha";
            var command = new LoginUsuarioCommand(email, "senhaErrada");
            var authEntity = new UsuarioAutenticacao(Guid.NewGuid(), email, senhaHash);

            _authRepositoryMock.Setup(x => x.ObterPorEmailAsync(email)).ReturnsAsync(authEntity);
            _criptografiaServiceMock.Setup(x => x.VerificarSenha(command.Senha, senhaHash)).Returns(false);

            var handler = new LoginUsuarioHandler(
                _usuarioRepositoryMock.Object,
                _authRepositoryMock.Object,
                _criptografiaServiceMock.Object,
                _tokenServiceMock.Object
            );

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command));
            Assert.Equal("E-mail ou senha inválidos.", exception.Message);
        }

        [Fact]
        public async Task Handle_UsuarioNaoEncontrado_DeveLancarInvalidOperationException()
        {
            var email = "email@teste.com";
            var senha = "senha123";
            var senhaHash = "hash_da_senha";
            var usuarioId = Guid.NewGuid();

            var command = new LoginUsuarioCommand(email, senha);
            var authEntity = new UsuarioAutenticacao(usuarioId, email, senhaHash);

            _authRepositoryMock.Setup(x => x.ObterPorEmailAsync(email)).ReturnsAsync(authEntity);
            _criptografiaServiceMock.Setup(x => x.VerificarSenha(senha, senhaHash)).Returns(true);
            _usuarioRepositoryMock.Setup(x => x.ObterPorIdAsync(usuarioId)).ReturnsAsync((KDramaSystem.Domain.Entities.Usuario)null);

            var handler = new LoginUsuarioHandler(
                _usuarioRepositoryMock.Object,
                _authRepositoryMock.Object,
                _criptografiaServiceMock.Object,
                _tokenServiceMock.Object
            );

            var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command));
            Assert.Equal("Usuário não encontrado.", exception.Message);
        }
    }
}