using System.Security.Claims;
using KDramaSystem.Application.UseCases.Avaliacao.Excluir;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.ValueObjetcs;
using Microsoft.AspNetCore.Http;
using Moq;

public class ExcluirAvaliacaoUseCaseTests
{
    private readonly Mock<IAvaliacaoRepository> _avaliacaoRepositoryMock = new();
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock = new();
    private readonly ExcluirAvaliacaoValidator _validator = new();

    private ExcluirAvaliacaoUseCase CriarUseCase()
    {
        return new ExcluirAvaliacaoUseCase(_avaliacaoRepositoryMock.Object, _httpContextAccessorMock.Object, _validator);
    }

    private void SetupHttpContext(Guid? userId)
    {
        var claims = userId.HasValue
            ? new[] { new Claim(ClaimTypes.NameIdentifier, userId.Value.ToString()) }
            : Array.Empty<Claim>();

        var identity = new ClaimsIdentity(claims, "testAuthType");
        var principal = new ClaimsPrincipal(identity);

        var httpContextMock = new Mock<HttpContext>();
        httpContextMock.Setup(x => x.User).Returns(principal);

        _httpContextAccessorMock.Setup(x => x.HttpContext).Returns(httpContextMock.Object);
    }

    [Fact]
    public async Task ExecutarAsync_ComRequestValido_DeveRemoverAvaliacao()
    {
        var userId = Guid.NewGuid();
        SetupHttpContext(userId);

        var temporadaId = Guid.NewGuid();
        var avaliacao = new Avaliacao(
            Guid.NewGuid(),
            userId,
            temporadaId,
            new Nota(3),
            comentario: null,
            recomendadoPorUsuarioId: null,
            recomendadoPorNomeLivre: null
        );

        _avaliacaoRepositoryMock
            .Setup(r => r.ObterPorUsuarioETemporadaAsync(userId, temporadaId))
            .ReturnsAsync(avaliacao);

        var request = new ExcluirAvaliacaoRequest { TemporadaId = temporadaId };

        var useCase = CriarUseCase();

        await useCase.ExecuteAsync(request);

        _avaliacaoRepositoryMock.Verify(r => r.RemoverAsync(avaliacao), Times.Once);
    }

    [Fact]
    public async Task ExecutarAsync_RequestInvalido_DeveLancarExcecaoComErroDeValidacao()
    {
        var userId = Guid.NewGuid();
        SetupHttpContext(userId);

        var request = new ExcluirAvaliacaoRequest { TemporadaId = Guid.Empty };

        var useCase = CriarUseCase();

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Contains("A temporada é obrigatória.", ex.Message);

        _avaliacaoRepositoryMock.Verify(r => r.RemoverAsync(It.IsAny<Avaliacao>()), Times.Never);
    }

    [Fact]
    public async Task ExecutarAsync_AvaliacaoNaoEncontrada_DeveLancarExcecao()
    {
        var userId = Guid.NewGuid();
        SetupHttpContext(userId);

        var temporadaId = Guid.NewGuid();

        _avaliacaoRepositoryMock
            .Setup(r => r.ObterPorUsuarioETemporadaAsync(userId, temporadaId))
            .ReturnsAsync((Avaliacao?)null);

        var request = new ExcluirAvaliacaoRequest { TemporadaId = temporadaId };

        var useCase = CriarUseCase();

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Avaliação não encontrada.", ex.Message);

        _avaliacaoRepositoryMock.Verify(r => r.RemoverAsync(It.IsAny<Avaliacao>()), Times.Never);
    }

    [Fact]
    public async Task ExecutarAsync_UsuarioNaoAutenticado_DeveLancarExcecao()
    {
        SetupHttpContext(null);

        var request = new ExcluirAvaliacaoRequest { TemporadaId = Guid.NewGuid() };

        var useCase = CriarUseCase();

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Usuário não autenticado.", ex.Message);

        _avaliacaoRepositoryMock.Verify(r => r.RemoverAsync(It.IsAny<Avaliacao>()), Times.Never);
    }
}