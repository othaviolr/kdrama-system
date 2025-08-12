using KDramaSystem.Application.UseCases.ProgressoTemporada.ExcluirProgresso;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Enums;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.ValueObjects;
using Moq;

public class ExcluirProgressoTemporadaUseCaseTests
{
    private readonly Mock<IProgressoTemporadaRepository> _progressoRepositoryMock = new();

    private ExcluirProgressoTemporadaUseCase CriarUseCase()
    {
        return new ExcluirProgressoTemporadaUseCase(_progressoRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ComRequestValido_DeveExcluirProgresso()
    {
        var usuarioId = Guid.NewGuid();
        var temporadaId = Guid.NewGuid();
        var progresso = new ProgressoTemporada(
            Guid.NewGuid(),
            usuarioId,
            temporadaId,
            5,
            new StatusDorama(StatusDoramaEnum.Assistindo));

        _progressoRepositoryMock
            .Setup(r => r.ObterPorUsuarioETemporadaAsync(usuarioId, temporadaId))
            .ReturnsAsync(progresso);

        _progressoRepositoryMock
            .Setup(r => r.ExcluirAsync(progresso.Id))
            .Returns(Task.CompletedTask);

        var useCase = CriarUseCase();

        var request = new ExcluirProgressoTemporadaRequest
        {
            TemporadaId = temporadaId
        };

        await useCase.ExecuteAsync(usuarioId, request);

        _progressoRepositoryMock.Verify(r => r.ExcluirAsync(progresso.Id), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ProgressoNaoEncontrado_DeveLancarExcecao()
    {
        var usuarioId = Guid.NewGuid();
        var temporadaId = Guid.NewGuid();

        _progressoRepositoryMock
            .Setup(r => r.ObterPorUsuarioETemporadaAsync(usuarioId, temporadaId))
            .ReturnsAsync((ProgressoTemporada?)null);

        var useCase = CriarUseCase();

        var request = new ExcluirProgressoTemporadaRequest
        {
            TemporadaId = temporadaId
        };

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(usuarioId, request));
        Assert.Equal("Progresso da temporada não encontrado.", ex.Message);

        _progressoRepositoryMock.Verify(r => r.ExcluirAsync(It.IsAny<Guid>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_RequestInvalido_DeveLancarExcecaoDeValidacao()
    {
        var usuarioId = Guid.NewGuid();

        var useCase = CriarUseCase();

        var request = new ExcluirProgressoTemporadaRequest
        {
            TemporadaId = Guid.Empty
        };

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(usuarioId, request));
        Assert.Contains("O Id da temporada é obrigatório.", ex.Message);

        _progressoRepositoryMock.Verify(r => r.ExcluirAsync(It.IsAny<Guid>()), Times.Never);
    }
}