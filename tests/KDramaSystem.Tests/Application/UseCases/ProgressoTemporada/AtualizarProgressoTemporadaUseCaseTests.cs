using KDramaSystem.Application.UseCases.ProgressoTemporada.AtualizarProgresso;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.ValueObjects;
using Moq;

public class AtualizarProgressoTemporadaUseCaseTests
{
    private readonly Mock<IProgressoTemporadaRepository> _progressoRepositoryMock = new();
    private readonly Mock<ITemporadaRepository> _temporadaRepositoryMock = new();

    private AtualizarProgressoTemporadaUseCase CriarUseCase()
    {
        return new AtualizarProgressoTemporadaUseCase(_progressoRepositoryMock.Object, _temporadaRepositoryMock.Object);
    }

    [Fact]
    public async Task ExecuteAsync_RequestInvalido_DeveLancarExcecao()
    {
        var useCase = CriarUseCase();
        var usuarioId = Guid.NewGuid();

        var request = new AtualizarProgressoTemporadaRequest
        {
            TemporadaId = Guid.Empty,
            EpisodiosAssistidos = -1
        };

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(usuarioId, request));
        Assert.Contains("O Id da temporada é obrigatório.", ex.Message);
        Assert.Contains("A quantidade de episódios assistidos não pode ser negativa.", ex.Message);
    }

    [Fact]
    public async Task ExecuteAsync_TemporadaNaoEncontrada_DeveLancarExcecao()
    {
        var useCase = CriarUseCase();
        var usuarioId = Guid.NewGuid();
        var temporadaId = Guid.NewGuid();

        var request = new AtualizarProgressoTemporadaRequest
        {
            TemporadaId = temporadaId,
            EpisodiosAssistidos = 2
        };

        _temporadaRepositoryMock.Setup(r => r.ObterPorIdAsync(temporadaId)).ReturnsAsync((Temporada?)null);

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(usuarioId, request));
        Assert.Equal("Temporada não encontrada.", ex.Message);
    }

    [Fact]
    public async Task ExecuteAsync_ProgressoNaoExiste_DeveCriarEAtualizarProgresso()
    {
        var useCase = CriarUseCase();
        var usuarioId = Guid.NewGuid();
        var temporadaId = Guid.NewGuid();

        var request = new AtualizarProgressoTemporadaRequest
        {
            TemporadaId = temporadaId,
            EpisodiosAssistidos = 3
        };

        _progressoRepositoryMock.Setup(r => r.ObterPorUsuarioETemporadaAsync(usuarioId, temporadaId)).ReturnsAsync((ProgressoTemporada?)null);
        _temporadaRepositoryMock.Setup(r => r.ObterPorIdAsync(temporadaId))
            .ReturnsAsync(new Temporada(
                id: temporadaId,
                doramaId: Guid.NewGuid(),
                numero: 1,
                anoLancamento: 2023,
                emExibicao: true,
                nome: "Temporada 1",
                sinopse: "Sinopse da temporada 1"
            ));
        _temporadaRepositoryMock.Setup(r => r.ContarEpisodiosAsync(temporadaId)).ReturnsAsync(10);
        _progressoRepositoryMock.Setup(r => r.CriarAsync(It.IsAny<ProgressoTemporada>())).Returns(Task.CompletedTask);
        _progressoRepositoryMock.Setup(r => r.AtualizarAsync(It.IsAny<ProgressoTemporada>())).Returns(Task.CompletedTask);

        await useCase.ExecuteAsync(usuarioId, request);

        _progressoRepositoryMock.Verify(r => r.CriarAsync(It.IsAny<ProgressoTemporada>()), Times.Once);
        _progressoRepositoryMock.Verify(r => r.AtualizarAsync(It.IsAny<ProgressoTemporada>()), Times.Once);
    }

    [Fact]
    public async Task ExecuteAsync_ProgressoExiste_DeveApenasAtualizar()
    {
        var useCase = CriarUseCase();
        var usuarioId = Guid.NewGuid();
        var temporadaId = Guid.NewGuid();

        var progressoExistente = new ProgressoTemporada(
            Guid.NewGuid(),
            usuarioId,
            temporadaId,
            1,
            new StatusDorama(KDramaSystem.Domain.Enums.StatusDoramaEnum.PlanejoAssistir));

        var request = new AtualizarProgressoTemporadaRequest
        {
            TemporadaId = temporadaId,
            EpisodiosAssistidos = 4
        };

        _progressoRepositoryMock.Setup(r => r.ObterPorUsuarioETemporadaAsync(usuarioId, temporadaId)).ReturnsAsync(progressoExistente);
        _temporadaRepositoryMock.Setup(r => r.ObterPorIdAsync(temporadaId))
            .ReturnsAsync(new Temporada(
                id: temporadaId,
                doramaId: Guid.NewGuid(),
                numero: 1,
                anoLancamento: 2023,
                emExibicao: true,
                nome: "Temporada 1",
                sinopse: "Sinopse da temporada 1"
            ));
        _temporadaRepositoryMock.Setup(r => r.ContarEpisodiosAsync(temporadaId)).ReturnsAsync(10);
        _progressoRepositoryMock.Setup(r => r.AtualizarAsync(progressoExistente)).Returns(Task.CompletedTask);

        await useCase.ExecuteAsync(usuarioId, request);

        _progressoRepositoryMock.Verify(r => r.CriarAsync(It.IsAny<ProgressoTemporada>()), Times.Never);
        _progressoRepositoryMock.Verify(r => r.AtualizarAsync(progressoExistente), Times.Once);
    }
}