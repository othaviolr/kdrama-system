using FluentValidation;
using KDramaSystem.Application.UseCases.ListaPrateleira.Editar;
using KDramaSystem.Domain.Enums;
using KDramaSystem.Domain.Interfaces.Repositories;
using Moq;

public class EditarListaPrateleiraUseCaseTests
{
    private readonly Mock<IListaPrateleiraRepository> _repoMock = new();
    private readonly Mock<IValidator<EditarListaPrateleiraRequest>> _validatorMock = new();

    private EditarListaPrateleiraUseCase CriarUseCase()
    {
        return new EditarListaPrateleiraUseCase(_repoMock.Object, _validatorMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_RequestValido_DeveAtualizarCampos()
    {
        var lista = new KDramaSystem.Domain.Entities.ListaPrateleira(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Lista Original",
            ListaPrivacidade.Publico,
            "Descricao Original",
            "url_original"
        );

        _repoMock.Setup(r => r.ObterPorIdAsync(lista.Id, It.IsAny<CancellationToken>())).ReturnsAsync(lista);
        _validatorMock.Setup(v => v.ValidateAndThrowAsync(It.IsAny<EditarListaPrateleiraRequest>(), It.IsAny<CancellationToken>()))
                      .Returns(Task.CompletedTask);

        var useCase = CriarUseCase();

        var request = new EditarListaPrateleiraRequest
        {
            ListaId = lista.Id,
            UsuarioId = lista.UsuarioId,
            Nome = "Lista Atualizada",
            Descricao = "Descricao Atualizada",
            ImagemCapaUrl = "url_atualizada",
            Privacidade = ListaPrivacidade.Privado
        };

        var listaAtualizada = await useCase.ExecuteAsync(request);

        _validatorMock.Verify(v => v.ValidateAndThrowAsync(request, It.IsAny<CancellationToken>()), Times.Once);
        _repoMock.Verify(r => r.AtualizarAsync(lista, It.IsAny<CancellationToken>()), Times.Once);

        Assert.Equal(request.Nome, listaAtualizada.Nome);
        Assert.Equal(request.Descricao, listaAtualizada.Descricao);
        Assert.Equal(request.ImagemCapaUrl, listaAtualizada.ImagemCapaUrl);
        Assert.Equal(request.Privacidade, listaAtualizada.Privacidade);
    }

    [Fact]
    public async Task ExecuteAsync_ListaNaoEncontrada_DeveLancarExcecao()
    {
        var listaId = Guid.NewGuid();
        _repoMock.Setup(r => r.ObterPorIdAsync(listaId, It.IsAny<CancellationToken>())).ReturnsAsync((KDramaSystem.Domain.Entities.ListaPrateleira?)null);

        var useCase = CriarUseCase();
        var request = new EditarListaPrateleiraRequest
        {
            ListaId = listaId,
            UsuarioId = Guid.NewGuid(),
            Nome = "Nova Lista"
        };

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Lista não encontrada ou usuário não autorizado.", ex.Message);
        _repoMock.Verify(r => r.AtualizarAsync(It.IsAny<KDramaSystem.Domain.Entities.ListaPrateleira>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_UsuarioNaoAutorizado_DeveLancarExcecao()
    {
        var lista = new KDramaSystem.Domain.Entities.ListaPrateleira(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Lista Original",
            ListaPrivacidade.Publico,
            "Descricao Original",
            "url_original"
        );

        _repoMock.Setup(r => r.ObterPorIdAsync(lista.Id, It.IsAny<CancellationToken>())).ReturnsAsync(lista);

        var useCase = CriarUseCase();
        var request = new EditarListaPrateleiraRequest
        {
            ListaId = lista.Id,
            UsuarioId = Guid.NewGuid(),
            Nome = "Lista Atualizada"
        };

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Lista não encontrada ou usuário não autorizado.", ex.Message);
        _repoMock.Verify(r => r.AtualizarAsync(It.IsAny<KDramaSystem.Domain.Entities.ListaPrateleira>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_ValidatorFalha_DeveLancarExcecao()
    {
        var lista = new KDramaSystem.Domain.Entities.ListaPrateleira(
            Guid.NewGuid(),
            Guid.NewGuid(),
            "Lista Original",
            ListaPrivacidade.Publico,
            "Descricao Original",
            "url_original"
        );

        _repoMock.Setup(r => r.ObterPorIdAsync(lista.Id, It.IsAny<CancellationToken>())).ReturnsAsync(lista);
        _validatorMock.Setup(v => v.ValidateAndThrowAsync(It.IsAny<EditarListaPrateleiraRequest>(), It.IsAny<CancellationToken>()))
                      .ThrowsAsync(new Exception("Falha de validação"));

        var useCase = CriarUseCase();
        var request = new EditarListaPrateleiraRequest
        {
            ListaId = lista.Id,
            UsuarioId = lista.UsuarioId,
            Nome = ""
        };

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Falha de validação", ex.Message);
        _repoMock.Verify(r => r.AtualizarAsync(It.IsAny<KDramaSystem.Domain.Entities.ListaPrateleira>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}