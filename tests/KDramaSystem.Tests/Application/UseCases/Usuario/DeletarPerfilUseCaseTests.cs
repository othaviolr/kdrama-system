using KDramaSystem.Application.Interfaces;
using KDramaSystem.Application.UseCases.Usuario.Deletar;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces.Repositories;
using Moq;

public class DeletarPerfilUseCaseTests
{
    private readonly Mock<IUsuarioRepository> _usuarioRepoMock;
    private readonly Mock<IUsuarioAutenticadoProvider> _usuarioAuthProviderMock;
    private readonly DeletarPerfilUseCase _useCase;

    public DeletarPerfilUseCaseTests()
    {
        _usuarioRepoMock = new Mock<IUsuarioRepository>();
        _usuarioAuthProviderMock = new Mock<IUsuarioAutenticadoProvider>();

        _useCase = new DeletarPerfilUseCase(_usuarioRepoMock.Object, _usuarioAuthProviderMock.Object);
    }

    [Fact]
    public async Task ExecutarAsync_UsuarioEncontrado_DeveRemoverUsuario()
    {
        var usuarioId = Guid.NewGuid();
        var usuario = new Usuario(usuarioId, "Usuário", "usuariokdrama", "email@exemplo.com");

        _usuarioAuthProviderMock.Setup(x => x.ObterUsuarioId()).Returns(usuarioId);
        _usuarioRepoMock.Setup(x => x.ObterPorIdAsync(usuarioId)).ReturnsAsync(usuario);
        _usuarioRepoMock.Setup(x => x.RemoverAsync(usuario.Id)).Returns(Task.CompletedTask);

        await _useCase.ExecutarAsync();

        _usuarioRepoMock.Verify(x => x.RemoverAsync(usuario.Id), Times.Once);
    }

    [Fact]
    public async Task ExecutarAsync_UsuarioNaoEncontrado_DeveLancarExcecao()
    {
        var usuarioId = Guid.NewGuid();

        _usuarioAuthProviderMock.Setup(x => x.ObterUsuarioId()).Returns(usuarioId);
        _usuarioRepoMock.Setup(x => x.ObterPorIdAsync(usuarioId)).ReturnsAsync((Usuario?)null);

        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => _useCase.ExecutarAsync());
        Assert.Equal("Usuário não encontrado.", ex.Message);

        _usuarioRepoMock.Verify(x => x.RemoverAsync(It.IsAny<Guid>()), Times.Never);
    }
}