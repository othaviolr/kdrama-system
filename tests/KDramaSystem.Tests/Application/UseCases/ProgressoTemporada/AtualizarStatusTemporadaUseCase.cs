using KDramaSystem.Application.UseCases.ProgressoTemporada.AtualizarStatus;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Enums;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.ValueObjects;
using Moq;

public class AtualizarStatusTemporadaUseCaseTests
{
    private readonly Mock<IProgressoTemporadaRepository> _progressoRepositoryMock = new();

    private AtualizarStatusTemporadaUseCase CriarUseCase()
    {
        return new AtualizarStatusTemporadaUseCase(_progressoRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_ProgressoNaoExiste_DeveCriarNovo()
    {
        var usuarioId = Guid.NewGuid();
        var temporadaId = Guid.NewGuid();

        var request = new AtualizarStatusTemporadaRequest
        {
            TemporadaId = temporadaId,
            Status = StatusDoramaEnum.Assistindo
        };

        _progressoRepositoryMock
            .Setup(r => r.ObterPorUsuarioETemporadaAsync(usuarioId, temporadaId))
            .ReturnsAsync((ProgressoTemporada?)null);

        _progressoRepositoryMock
            .Setup(r => r.CriarAsync(It.IsAny<ProgressoTemporada>()))
            .Returns(Task.CompletedTask);

        var useCase = CriarUseCase();

        await useCase.ExecuteAsync(usuarioId, request);

        _progressoRepositoryMock.Verify(r => r.CriarAsync(It.Is<ProgressoTemporada>(
            p => p.UsuarioId == usuarioId
              && p.TemporadaId == temporadaId
              && p.Status.Valor == StatusDoramaEnum.Assistindo)), Times.Once);

        _progressoRepositoryMock.Verify(r => r.AtualizarAsync(It.IsAny<ProgressoTemporada>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_ProgressoExiste_DeveAtualizarStatus()
    {
        var usuarioId = Guid.NewGuid();
        var temporadaId = Guid.NewGuid();

        var progressoExistente = new ProgressoTemporada(
            Guid.NewGuid(),
            usuarioId,
            temporadaId,
            2,
            new StatusDorama(StatusDoramaEnum.PlanejoAssistir));

        var request = new AtualizarStatusTemporadaRequest
        {
            TemporadaId = temporadaId,
            Status = StatusDoramaEnum.Concluido
        };

        _progressoRepositoryMock
            .Setup(r => r.ObterPorUsuarioETemporadaAsync(usuarioId, temporadaId))
            .ReturnsAsync(progressoExistente);

        _progressoRepositoryMock
            .Setup(r => r.AtualizarAsync(progressoExistente))
            .Returns(Task.CompletedTask);

        var useCase = CriarUseCase();

        await useCase.ExecuteAsync(usuarioId, request);

        Assert.Equal(StatusDoramaEnum.Concluido, progressoExistente.Status.Valor);

        _progressoRepositoryMock.Verify(r => r.AtualizarAsync(progressoExistente), Times.Once);
        _progressoRepositoryMock.Verify(r => r.CriarAsync(It.IsAny<ProgressoTemporada>()), Times.Never);
    }

    [Fact]
    public async Task ExecuteAsync_RequestInvalido_DeveLancarExcecao()
    {
        var useCase = CriarUseCase();
        var usuarioId = Guid.NewGuid();

        var request = new AtualizarStatusTemporadaRequest
        {
            TemporadaId = Guid.Empty,
            Status = (StatusDoramaEnum)999
        };

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(usuarioId, request));

        Assert.Contains("O Id da temporada é obrigatório.", ex.Message);
        Assert.Contains("Status é inválido.", ex.Message);
    }
}