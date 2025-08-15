using FluentValidation;
using KDramaSystem.Application.UseCases.ListaPrateleira.Criar;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Enums;
using KDramaSystem.Domain.Interfaces.Repositories;
using Moq;

public class CriarListaPrateleiraUseCaseTests
{
    private readonly Mock<IListaPrateleiraRepository> _repoMock = new();
    private readonly Mock<IValidator<CriarListaPrateleiraRequest>> _validatorMock = new();

    private CriarListaPrateleiraUseCase CriarUseCase() => new CriarListaPrateleiraUseCase(_repoMock.Object, _validatorMock.Object);

    [Fact]
    public async Task ExecuteAsync_RequestValido_DeveCriarLista()
    {
        _validatorMock.Setup(v => v.ValidateAndThrowAsync(It.IsAny<CriarListaPrateleiraRequest>(), It.IsAny<CancellationToken>()))
                      .Returns(Task.CompletedTask);

        var request = new CriarListaPrateleiraRequest
        {
            UsuarioId = Guid.NewGuid(),
            Nome = "Lista Teste",
            Descricao = "Descrição teste",
            Privacidade = ListaPrivacidade.Publico,
            ImagemCapaUrl = "url_teste"
        };

        var useCase = CriarUseCase();
        var lista = await useCase.ExecuteAsync(request);

        _validatorMock.Verify(v => v.ValidateAndThrowAsync(request, It.IsAny<CancellationToken>()), Times.Once);
        _repoMock.Verify(r => r.AdicionarAsync(It.IsAny<ListaPrateleira>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.Equal(request.Nome, lista.Nome);
        Assert.Equal(request.Descricao, lista.Descricao);
        Assert.Equal(request.Privacidade, lista.Privacidade);
        Assert.Equal(request.ImagemCapaUrl, lista.ImagemCapaUrl);
        Assert.Equal(request.UsuarioId, lista.UsuarioId);
    }

    [Fact]
    public async Task ExecuteAsync_RequestComCamposOpcionaisNulos_DeveCriarLista()
    {
        _validatorMock.Setup(v => v.ValidateAndThrowAsync(It.IsAny<CriarListaPrateleiraRequest>(), It.IsAny<CancellationToken>()))
                      .Returns(Task.CompletedTask);

        var request = new CriarListaPrateleiraRequest
        {
            UsuarioId = Guid.NewGuid(),
            Nome = "Lista Sem Descricao",
            Descricao = null,
            Privacidade = ListaPrivacidade.Publico,
            ImagemCapaUrl = null
        };

        var useCase = CriarUseCase();
        var lista = await useCase.ExecuteAsync(request);

        _repoMock.Verify(r => r.AdicionarAsync(It.IsAny<ListaPrateleira>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.Null(lista.Descricao);
        Assert.Null(lista.ImagemCapaUrl);
    }

    [Fact]
    public async Task ExecuteAsync_ValidatorFalha_DeveLancarExcecao()
    {
        _validatorMock.Setup(v => v.ValidateAndThrowAsync(It.IsAny<CriarListaPrateleiraRequest>(), It.IsAny<CancellationToken>()))
                      .ThrowsAsync(new Exception("Falha de validação"));

        var request = new CriarListaPrateleiraRequest
        {
            UsuarioId = Guid.NewGuid(),
            Nome = "",
            Descricao = "Descrição",
            Privacidade = ListaPrivacidade.Publico
        };

        var useCase = CriarUseCase();

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Falha de validação", ex.Message);
        _repoMock.Verify(r => r.AdicionarAsync(It.IsAny<ListaPrateleira>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_UsuarioIdInvalido_DeveLancarExcecao()
    {
        _validatorMock.Setup(v => v.ValidateAndThrowAsync(It.IsAny<CriarListaPrateleiraRequest>(), It.IsAny<CancellationToken>()))
                      .Returns(Task.CompletedTask);

        var request = new CriarListaPrateleiraRequest
        {
            UsuarioId = Guid.Empty,
            Nome = "Lista Teste",
            Descricao = "Descrição",
            Privacidade = ListaPrivacidade.Publico
        };

        var useCase = CriarUseCase();
        await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync(request));
    }

    [Fact]
    public async Task ExecuteAsync_RepositorioLancaExcecao_DevePropagar()
    {
        _validatorMock.Setup(v => v.ValidateAndThrowAsync(It.IsAny<CriarListaPrateleiraRequest>(), It.IsAny<CancellationToken>()))
                      .Returns(Task.CompletedTask);

        var request = new CriarListaPrateleiraRequest
        {
            UsuarioId = Guid.NewGuid(),
            Nome = "Lista Teste",
            Descricao = "Descrição",
            Privacidade = ListaPrivacidade.Publico
        };

        _repoMock.Setup(r => r.AdicionarAsync(It.IsAny<ListaPrateleira>(), It.IsAny<CancellationToken>()))
                 .ThrowsAsync(new Exception("Falha no banco"));

        var useCase = CriarUseCase();
        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Falha no banco", ex.Message);
    }
}