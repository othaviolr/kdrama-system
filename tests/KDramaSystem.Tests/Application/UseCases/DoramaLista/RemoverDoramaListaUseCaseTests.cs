using KDramaSystem.Application.UseCases.DoramaLista.Remover;
using KDramaSystem.Domain.Interfaces.Repositories;
using Moq;

public class RemoverDoramaListaUseCaseTests
{
    private readonly Mock<IDoramaListaRepository> _repoMock = new();

    private RemoverDoramaListaUseCase CriarUseCase()
    {
        return new RemoverDoramaListaUseCase(_repoMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ComIdsValidos_DeveRemoverDoramaDaLista()
    {
        var listaPrateleiraId = Guid.NewGuid();
        var doramaId = Guid.NewGuid();
        _repoMock.Setup(r => r.RemoverAsync(listaPrateleiraId, doramaId)).Returns(Task.CompletedTask);

        var useCase = CriarUseCase();

        await useCase.ExecuteAsync(listaPrateleiraId, doramaId);

        _repoMock.Verify(r => r.RemoverAsync(listaPrateleiraId, doramaId), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ComListaIdInvalido_DeveLancarExcecao()
    {
        var doramaId = Guid.NewGuid();
        var useCase = CriarUseCase();

        await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(Guid.Empty, doramaId));
    }

    [Fact]
    public async Task ExecuteAsync_ComDoramaIdInvalido_DeveLancarExcecao()
    {
        var listaPrateleiraId = Guid.NewGuid();
        var useCase = CriarUseCase();

        await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(listaPrateleiraId, Guid.Empty));
    }

    [Fact]
    public async Task ExecuteAsync_RepositorioLancaExcecao_DevePropagar()
    {
        var listaPrateleiraId = Guid.NewGuid();
        var doramaId = Guid.NewGuid();
        _repoMock.Setup(r => r.RemoverAsync(listaPrateleiraId, doramaId))
                 .ThrowsAsync(new Exception("Falha no banco"));

        var useCase = CriarUseCase();

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(listaPrateleiraId, doramaId));
        Assert.Equal("Falha no banco", ex.Message);
    }
}