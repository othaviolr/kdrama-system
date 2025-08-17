using FluentValidation;
using FluentValidation.Results;
using KDramaSystem.Application.UseCases.ListaPrateleira.Excluir;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces.Repositories;
using Moq;

public class ExcluirListaPrateleiraUseCaseTests
{
    private readonly Mock<IListaPrateleiraRepository> _repoMock = new();
    private readonly Mock<IValidator<ExcluirListaPrateleiraRequest>> _validatorMock = new();

    private ExcluirListaPrateleiraUseCase CriarUseCase()
        => new(_repoMock.Object, _validatorMock.Object);

    [Fact]
    public async Task ExecuteAsync_RequestValido_DeveRemoverLista()
    {
        var lista = new ListaPrateleira(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Lista Teste",
            0,
            "Descricao",
            "url"
        );

        _repoMock.Setup(r => r.ObterPorIdAsync(lista.Id, It.IsAny<CancellationToken>())).ReturnsAsync(lista);
        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ExcluirListaPrateleiraRequest>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new ValidationResult());

        var useCase = CriarUseCase();
        var request = new ExcluirListaPrateleiraRequest
        {
            ListaId = lista.Id,
            UsuarioId = lista.UsuarioId
        };

        await useCase.ExecuteAsync(request);

        _validatorMock.Verify(v => v.ValidateAsync(request, It.IsAny<CancellationToken>()), Times.Once);
        _repoMock.Verify(r => r.RemoverAsync(lista.Id, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ListaNaoEncontrada_DeveLancarExcecao()
    {
        var listaId = Guid.NewGuid();
        _repoMock.Setup(r => r.ObterPorIdAsync(listaId, It.IsAny<CancellationToken>()))
                 .ReturnsAsync((ListaPrateleira?)null);

        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ExcluirListaPrateleiraRequest>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new ValidationResult());

        var useCase = CriarUseCase();
        var request = new ExcluirListaPrateleiraRequest { ListaId = listaId, UsuarioId = Guid.NewGuid() };

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Lista não encontrada.", ex.Message);
        _repoMock.Verify(r => r.RemoverAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_UsuarioNaoAutorizado_DeveLancarExcecao()
    {
        var donoListaId = Guid.NewGuid();
        var lista = new ListaPrateleira(
            Guid.NewGuid(),
            donoListaId,
            "Lista Teste",
            0,
            "Descricao",
            "url"
        );

        _repoMock.Setup(r => r.ObterPorIdAsync(lista.Id, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(lista);

        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ExcluirListaPrateleiraRequest>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new ValidationResult());

        var useCase = CriarUseCase();
        var request = new ExcluirListaPrateleiraRequest
        {
            ListaId = lista.Id,
            UsuarioId = Guid.NewGuid() // diferente do dono
        };

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Usuário não autorizado.", ex.Message);
        _repoMock.Verify(r => r.RemoverAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_ValidatorFalha_DeveLancarValidationException()
    {
        var lista = new ListaPrateleira(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Lista Teste",
            0,
            "Descricao",
            "url"
        );

        _repoMock.Setup(r => r.ObterPorIdAsync(lista.Id, It.IsAny<CancellationToken>()))
                 .ReturnsAsync(lista);

        _validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ExcluirListaPrateleiraRequest>(), It.IsAny<CancellationToken>()))
                      .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure("ListaId", "Campo obrigatório") }));

        var useCase = CriarUseCase();
        var request = new ExcluirListaPrateleiraRequest
        {
            ListaId = lista.Id,
            UsuarioId = lista.UsuarioId
        };

        var ex = await Assert.ThrowsAsync<ValidationException>(() => useCase.ExecuteAsync(request));
        Assert.Contains("Campo obrigatório", ex.Message);
        _repoMock.Verify(r => r.RemoverAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}