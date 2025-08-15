using KDramaSystem.Application.UseCases.ListaPrateleira.Obter;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Enums;
using KDramaSystem.Domain.Interfaces.Repositories;
using Moq;

public class ObterListaPrateleiraUseCaseTests
{
    private readonly Mock<IListaPrateleiraRepository> _repoMock = new();

    private ObterListaPrateleiraUseCase CriarUseCase()
    {
        return new ObterListaPrateleiraUseCase(_repoMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ComListaId_RetornaLista()
    {
        var lista = new ListaPrateleira(Guid.NewGuid(), Guid.NewGuid(), "Lista Teste", ListaPrivacidade.Publico, "Descricao", "url");
        _repoMock.Setup(r => r.ObterPorIdAsync(lista.Id, It.IsAny<CancellationToken>())).ReturnsAsync(lista);

        var useCase = CriarUseCase();
        var request = new ObterListaPrateleiraRequest { ListaId = lista.Id };

        var result = await useCase.ExecuteAsync(request);

        Assert.Single(result);
        Assert.Equal(lista.Id, result.First().Id);
    }

    [Fact]
    public async Task ExecuteAsync_ComShareToken_RetornaLista()
    {
        var lista = new ListaPrateleira(Guid.NewGuid(), Guid.NewGuid(), "Lista Teste", ListaPrivacidade.Publico, "Descricao", "url");
        var token = "token123";
        _repoMock.Setup(r => r.ObterPorTokenAsync(token, It.IsAny<CancellationToken>())).ReturnsAsync(lista);

        var useCase = CriarUseCase();
        var request = new ObterListaPrateleiraRequest { ShareToken = token };

        var result = await useCase.ExecuteAsync(request);

        Assert.Single(result);
        Assert.Equal(lista.Id, result.First().Id);
    }

    [Fact]
    public async Task ExecuteAsync_ShareTokenInvalido_DeveLancarExcecao()
    {
        _repoMock.Setup(r => r.ObterPorTokenAsync("token123", It.IsAny<CancellationToken>())).ReturnsAsync((ListaPrateleira?)null);

        var useCase = CriarUseCase();
        var request = new ObterListaPrateleiraRequest { ShareToken = "token123" };

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Lista não encontrada ou não compartilhada.", ex.Message);
    }

    [Fact]
    public async Task ExecuteAsync_UsuarioNaoAutorizado_DeveLancarExcecao()
    {
        var lista = new ListaPrateleira(Guid.NewGuid(), Guid.NewGuid(), "Lista Teste", ListaPrivacidade.Privado, "Descricao", "url");
        var userId = Guid.NewGuid();
        _repoMock.Setup(r => r.ObterPorIdAsync(lista.Id, It.IsAny<CancellationToken>())).ReturnsAsync(lista);

        var useCase = CriarUseCase();
        var request = new ObterListaPrateleiraRequest { ListaId = lista.Id, UsuarioLogadoId = userId };

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Usuário não autorizado a acessar essa lista.", ex.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ComUsuarioId_RetornaListasDoUsuario()
    {
        var userId = Guid.NewGuid();
        var listas = new List<ListaPrateleira>
        {
            new ListaPrateleira(Guid.NewGuid(), userId, "Lista 1", ListaPrivacidade.Publico, "Desc", "url"),
            new ListaPrateleira(Guid.NewGuid(), userId, "Lista 2", ListaPrivacidade.Privado, "Desc", "url")
        };
        _repoMock.Setup(r => r.ObterPorUsuarioAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(listas);

        var useCase = CriarUseCase();
        var request = new ObterListaPrateleiraRequest { UsuarioId = userId };

        var result = await useCase.ExecuteAsync(request);

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task ExecuteAsync_ComUsuarioLogadoId_RetornaListasPublicas()
    {
        var userId = Guid.NewGuid();
        var listas = new List<ListaPrateleira>
        {
            new ListaPrateleira(Guid.NewGuid(), Guid.NewGuid(), "Lista Pública 1", ListaPrivacidade.Publico, "Desc", "url"),
            new ListaPrateleira(Guid.NewGuid(), Guid.NewGuid(), "Lista Pública 2", ListaPrivacidade.Publico, "Desc", "url")
        };
        _repoMock.Setup(r => r.ObterPublicasAsync(userId, It.IsAny<CancellationToken>())).ReturnsAsync(listas);

        var useCase = CriarUseCase();
        var request = new ObterListaPrateleiraRequest { UsuarioLogadoId = userId };

        var result = await useCase.ExecuteAsync(request);

        Assert.Equal(2, result.Count());
        Assert.All(result, l => Assert.Equal(ListaPrivacidade.Publico, l.Privacidade));
    }

    [Fact]
    public async Task ExecuteAsync_SemParametros_RetornaVazio()
    {
        var useCase = CriarUseCase();
        var request = new ObterListaPrateleiraRequest();

        var result = await useCase.ExecuteAsync(request);

        Assert.Empty(result);
    }
}