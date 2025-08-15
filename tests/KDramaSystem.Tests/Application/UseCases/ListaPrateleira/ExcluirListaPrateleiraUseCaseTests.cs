using FluentValidation;
using KDramaSystem.Application.UseCases.ListaPrateleira.Excluir;
using KDramaSystem.Domain.Interfaces.Repositories;
using Moq;

public class ExcluirListaPrateleiraUseCaseTests
{
    private readonly Mock<IListaPrateleiraRepository> _repoMock = new();
    private readonly Mock<IValidator<ExcluirListaPrateleiraRequest>> _validatorMock = new();

    private ExcluirListaPrateleiraUseCase CriarUseCase()
    {
        return new ExcluirListaPrateleiraUseCase(_repoMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_RequestValido_DeveRemoverLista()
    {
        var lista = new KDramaSystem.Domain.Entities.ListaPrateleira(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Lista Teste",
            0,
            "Descricao",
            "url"
        );

        _repoMock.Setup(r => r.ObterPorIdAsync(lista.Id, It.IsAny<CancellationToken>())).ReturnsAsync(lista);
        _validatorMock.Setup(v => v.ValidateAndThrowAsync(It.IsAny<ExcluirListaPrateleiraRequest>(), It.IsAny<CancellationToken>()))
                      .Returns(Task.CompletedTask);

        var useCase = CriarUseCase();
        var request = new ExcluirListaPrateleiraRequest
        {
            ListaId = lista.Id,
            UsuarioId = lista.UsuarioId
        };

        await useCase.ExecuteAsync(request);

        _validatorMock.Verify(v => v.ValidateAndThrowAsync(request, It.IsAny<CancellationToken>()), Times.Once);
        _repoMock.Verify(r => r.RemoverAsync(lista.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ListaNaoEncontrada_DeveLancarExcecao()
    {
        var listaId = Guid.NewGuid();
        _repoMock.Setup(r => r.ObterPorIdAsync(listaId, It.IsAny<CancellationToken>())).ReturnsAsync((KDramaSystem.Domain.Entities.ListaPrateleira?)null);

        var useCase = CriarUseCase();
        var request = new ExcluirListaPrateleiraRequest
        {
            ListaId = listaId,
            UsuarioId = Guid.NewGuid()
        };

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Lista não encontrada ou usuário não autorizado.", ex.Message);
        _repoMock.Verify(r => r.RemoverAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_UsuarioNaoAutorizado_DeveLancarExcecao()
    {
        var lista = new KDramaSystem.Domain.Entities.ListaPrateleira(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Lista Teste",
            0,
            "Descricao",
            "url"
        );

        _repoMock.Setup(r => r.ObterPorIdAsync(lista.Id, It.IsAny<CancellationToken>())).ReturnsAsync(lista);

        var useCase = CriarUseCase();
        var request = new ExcluirListaPrateleiraRequest
        {
            ListaId = lista.Id,
            UsuarioId = Guid.NewGuid()
        };

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Lista não encontrada ou usuário não autorizado.", ex.Message);
        _repoMock.Verify(r => r.RemoverAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_ValidatorFalha_DeveLancarExcecao()
    {
        var lista = new KDramaSystem.Domain.Entities.ListaPrateleira(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Lista Teste",
            0,
            "Descricao",
            "url"
        );

        _repoMock.Setup(r => r.ObterPorIdAsync(lista.Id, It.IsAny<CancellationToken>())).ReturnsAsync(lista);
        _validatorMock.Setup(v => v.ValidateAndThrowAsync(It.IsAny<ExcluirListaPrateleiraRequest>(), It.IsAny<CancellationToken>()))
                      .ThrowsAsync(new Exception("Falha de validação"));

        var useCase = CriarUseCase();
        var request = new ExcluirListaPrateleiraRequest
        {
            ListaId = lista.Id,
            UsuarioId = lista.UsuarioId
        };

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Falha de validação", ex.Message);
        _repoMock.Verify(r => r.RemoverAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}