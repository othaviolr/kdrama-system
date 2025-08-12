using System.Security.Claims;
using KDramaSystem.Application.UseCases.Avaliacao.Obter;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.ValueObjects;
using KDramaSystem.Domain.ValueObjetcs;
using Microsoft.AspNetCore.Http;
using Moq;

public class ObterAvaliacaoUseCaseTests
{
    private readonly Mock<IAvaliacaoRepository> _avaliacaoRepositoryMock = new();
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock = new();

    private ObterAvaliacaoUseCase CriarUseCase()
    {
        return new ObterAvaliacaoUseCase(_avaliacaoRepositoryMock.Object, _httpContextAccessorMock.Object);
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
    public async Task ExecutarAsync_AvaliacaoEncontrada_DeveRetornarDtoComDados()
    {
        var userId = Guid.NewGuid();
        var temporadaId = Guid.NewGuid();
        SetupHttpContext(userId);

        var avaliacao = new Avaliacao(
        Guid.NewGuid(),
        userId,
        temporadaId,
        new Nota(4),
        new ComentarioValor("Comentário teste"),
        recomendadoPorUsuarioId: Guid.NewGuid(),
        recomendadoPorNomeLivre: null
        );

        var data = DateTime.UtcNow;
        typeof(Avaliacao)
            .GetProperty("DataAvaliacao")
            ?.SetValue(avaliacao, data);

        _avaliacaoRepositoryMock
            .Setup(r => r.ObterPorUsuarioETemporadaAsync(userId, temporadaId))
            .ReturnsAsync(avaliacao);

        var useCase = CriarUseCase();

        var resultado = await useCase.ExecutarAsync(temporadaId);

        Assert.NotNull(resultado);
        Assert.Equal(avaliacao.Id, resultado.Id);
        Assert.Equal(avaliacao.TemporadaId, resultado.TemporadaId);
        Assert.Equal(avaliacao.Nota.Valor, resultado.Nota);
        Assert.Equal(avaliacao.Comentario?.Texto, resultado.Comentario);
        Assert.Equal(avaliacao.RecomendadoPorUsuarioId, resultado.RecomendadoPorUsuarioId);
        Assert.Equal(avaliacao.RecomendadoPorNomeLivre, resultado.RecomendadoPorNomeLivre);
        Assert.Equal(avaliacao.DataAvaliacao, resultado.DataAvaliacao);
    }

    [Fact]
    public async Task ExecutarAsync_AvaliacaoNaoEncontrada_DeveRetornarNull()
    {
        var userId = Guid.NewGuid();
        var temporadaId = Guid.NewGuid();
        SetupHttpContext(userId);

        _avaliacaoRepositoryMock
            .Setup(r => r.ObterPorUsuarioETemporadaAsync(userId, temporadaId))
            .ReturnsAsync((Avaliacao?)null);

        var useCase = CriarUseCase();

        var resultado = await useCase.ExecutarAsync(temporadaId);

        Assert.Null(resultado);
    }

    [Fact]
    public async Task ExecutarAsync_UsuarioNaoAutenticado_DeveLancarExcecao()
    {
        SetupHttpContext(null);
        var temporadaId = Guid.NewGuid();

        var useCase = CriarUseCase();

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecutarAsync(temporadaId));
        Assert.Equal("Usuário não autenticado.", ex.Message);
    }
}