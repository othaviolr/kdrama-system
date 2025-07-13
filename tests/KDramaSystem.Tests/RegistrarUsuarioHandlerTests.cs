using KDramaSystem.Application.UseCases.Usuario.Registrar;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces.Repositories;
using KDramaSystem.Domain.Interfaces.Services;
using Moq;

namespace KDramaSystem.Tests
{
    public class RegistrarUsuarioHandlerTests
    {
        private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock = new();
        private readonly Mock<IUsuarioAutenticacaoRepository> _authRepositoryMock = new();
        private readonly Mock<ICriptografiaService> _criptografiaServiceMock = new();
        private readonly Mock<ITokenService> _tokenServiceMock = new();

        [Fact]
        public async Task Handle_ComandoValido_DeveRegistrarUsuarioERetornarResultado()
        {
            var command = new RegistrarUsuarioCommand("Nome Teste", "nomeusuario", "email@teste.com", "senha123");

            _authRepositoryMock.Setup(x => x.EmailExisteAsync(command.Email)).ReturnsAsync(false);
            _usuarioRepositoryMock.Setup(x => x.NomeUsuarioExisteAsync(command.NomeUsuario)).ReturnsAsync(false);
            _criptografiaServiceMock.Setup(x => x.GerarHash(command.Senha)).Returns("hash_da_senha");
            _tokenServiceMock.Setup(x => x.GerarToken(It.IsAny<Guid>(), command.NomeUsuario, command.Email)).Returns("token123");

            var handler = new RegistrarUsuarioHandler(
                _usuarioRepositoryMock.Object,
                _authRepositoryMock.Object,
                _criptografiaServiceMock.Object,
                _tokenServiceMock.Object
            );

            var result = await handler.Handle(command);

            Assert.NotNull(result);
            Assert.Equal(command.Nome, result.Nome);
            Assert.Equal(command.NomeUsuario, result.NomeUsuario);
            Assert.Equal(command.Email, result.Email);
            Assert.Equal("token123", result.Token);

            _usuarioRepositoryMock.Verify(x => x.AdicionarAsync(It.IsAny<Usuario>()), Times.Once);
            _authRepositoryMock.Verify(x => x.SalvarAsync(It.IsAny<UsuarioAutenticacao>()), Times.Once);
        }

        [Fact]
        public async Task Handle_EmailJaExiste_DeveLancarInvalidOperationException()
        {
            var command = new RegistrarUsuarioCommand("Nome Teste", "nomeusuario", "email@teste.com", "senha123");
            _authRepositoryMock.Setup(x => x.EmailExisteAsync(command.Email)).ReturnsAsync(true);

            var handler = new RegistrarUsuarioHandler(
                _usuarioRepositoryMock.Object,
                _authRepositoryMock.Object,
                _criptografiaServiceMock.Object,
                _tokenServiceMock.Object
            );

            await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command));
        }

        [Fact]
        public async Task Handle_NomeUsuarioJaExiste_DeveLancarInvalidOperationException()
        {
            var command = new RegistrarUsuarioCommand("Nome Teste", "nomeusuario", "email@teste.com", "senha123");
            _authRepositoryMock.Setup(x => x.EmailExisteAsync(command.Email)).ReturnsAsync(false);
            _usuarioRepositoryMock.Setup(x => x.NomeUsuarioExisteAsync(command.NomeUsuario)).ReturnsAsync(true);

            var handler = new RegistrarUsuarioHandler(
                _usuarioRepositoryMock.Object,
                _authRepositoryMock.Object,
                _criptografiaServiceMock.Object,
                _tokenServiceMock.Object
            );

            await Assert.ThrowsAsync<InvalidOperationException>(() => handler.Handle(command));
        }
    }
}