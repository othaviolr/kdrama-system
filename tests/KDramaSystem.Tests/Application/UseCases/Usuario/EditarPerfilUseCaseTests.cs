using System;
using System.Threading.Tasks;
using KDramaSystem.Application.Interfaces;
using KDramaSystem.Application.UseCases.Usuario.Editar;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces.Repositories;
using Moq;
using Xunit;

public class EditarPerfilUseCaseTests
{
    private readonly Mock<IUsuarioRepository> _usuarioRepoMock;
    private readonly Mock<IUsuarioAutenticadoProvider> _usuarioAuthProviderMock;
    private readonly EditarPerfilUseCase _useCase;

    public EditarPerfilUseCaseTests()
    {
        _usuarioRepoMock = new Mock<IUsuarioRepository>();
        _usuarioAuthProviderMock = new Mock<IUsuarioAutenticadoProvider>();

        _useCase = new EditarPerfilUseCase(_usuarioRepoMock.Object, _usuarioAuthProviderMock.Object);
    }

    [Fact]
    public async Task ExecutarAsync_UsuarioEncontrado_DeveEditarESalvar()
    {
        var usuarioId = Guid.NewGuid();
        var usuario = new Usuario(usuarioId, "Nome Antigo", "nomeUsuario", "email@exemplo.com");

        _usuarioAuthProviderMock.Setup(x => x.ObterUsuarioId()).Returns(usuarioId);
        _usuarioRepoMock.Setup(x => x.ObterPorIdAsync(usuarioId)).ReturnsAsync(usuario);
        _usuarioRepoMock.Setup(x => x.SalvarAsync(usuario)).Returns(Task.CompletedTask);

        var request = new EditarPerfilRequest
        {
            Nome = "Nome Novo",
            NomeUsuario = "novoUsuario",
            FotoUrl = "urlDaFoto",
            Bio = "Nova bio"
        };

        await _useCase.ExecutarAsync(request);

        Assert.Equal("Nome Novo", usuario.Nome);
        Assert.Equal("novoUsuario", usuario.NomeUsuario);
        Assert.Equal("urlDaFoto", usuario.FotoUrl);
        Assert.Equal("Nova bio", usuario.Bio);

        _usuarioRepoMock.Verify(x => x.SalvarAsync(usuario), Times.Once);
    }

    [Fact]
    public async Task ExecutarAsync_UsuarioNaoEncontrado_DeveLancarException()
    {
        var usuarioId = Guid.NewGuid();

        _usuarioAuthProviderMock.Setup(x => x.ObterUsuarioId()).Returns(usuarioId);
        _usuarioRepoMock.Setup(x => x.ObterPorIdAsync(usuarioId)).ReturnsAsync((Usuario?)null);

        var request = new EditarPerfilRequest
        {
            Nome = "Nome Novo",
            NomeUsuario = "novoUsuario"
        };

        var ex = await Assert.ThrowsAsync<Exception>(() => _useCase.ExecutarAsync(request));
        Assert.Equal("Usuário não encontrado.", ex.Message);

        _usuarioRepoMock.Verify(x => x.SalvarAsync(It.IsAny<Usuario>()), Times.Never);
    }
}