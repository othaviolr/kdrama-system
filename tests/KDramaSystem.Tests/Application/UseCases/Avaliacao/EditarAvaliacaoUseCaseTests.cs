using System.Security.Claims;
using KDramaSystem.Application.UseCases.Avaliacao.Editar;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.ValueObjects;
using KDramaSystem.Domain.ValueObjetcs;
using Microsoft.AspNetCore.Http;
using Moq;

public class EditarAvaliacaoUseCaseTests
{
    private readonly Mock<IAvaliacaoRepository> _avaliacaoRepositoryMock = new();
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock = new();
    private readonly EditarAvaliacaoValidator _validator = new();

    private EditarAvaliacaoUseCase CriarUseCase()
    {
        return new EditarAvaliacaoUseCase(_avaliacaoRepositoryMock.Object, _httpContextAccessorMock.Object, _validator);
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
    public async Task ExecutarAsync_ComRequestValido_DeveAtualizarAvaliacao()
    {
        var userId = Guid.NewGuid();
        SetupHttpContext(userId);

        var request = new EditarAvaliacaoRequest
        {
            TemporadaId = Guid.NewGuid(),
            Nota = 4,
            Comentario = "Comentário atualizado",
            RecomendadoPorUsuarioId = null,
            RecomendadoPorNomeLivre = "Indicação atualizada"
        };

        var avaliacao = new Avaliacao(
            Guid.NewGuid(),
            userId,
            request.TemporadaId,
            new Nota(3),
            null,
            null,
            null
        );

        _avaliacaoRepositoryMock
            .Setup(r => r.ObterPorUsuarioETemporadaAsync(userId, request.TemporadaId))
            .ReturnsAsync(avaliacao);

        var useCase = CriarUseCase();

        await useCase.ExecuteAsync(request);

        _avaliacaoRepositoryMock.Verify(r => r.AtualizarAsync(avaliacao), Times.Once);
    }

    [Fact]
    public async Task ExecutarAsync_ComComentarioNulo_DeveAtualizarComentarioParaNulo()
    {
        var userId = Guid.NewGuid();
        SetupHttpContext(userId);

        var request = new EditarAvaliacaoRequest
        {
            TemporadaId = Guid.NewGuid(),
            Nota = 5,
            Comentario = null,
            RecomendadoPorUsuarioId = Guid.NewGuid(),
            RecomendadoPorNomeLivre = null
        };

        var avaliacao = new Avaliacao(
            Guid.NewGuid(),
            userId,
            request.TemporadaId,
            new Nota(4),
            new ComentarioValor("Comentário antigo"),
            null,
            null
        );

        _avaliacaoRepositoryMock
            .Setup(r => r.ObterPorUsuarioETemporadaAsync(userId, request.TemporadaId))
            .ReturnsAsync(avaliacao);

        var useCase = CriarUseCase();

        await useCase.ExecuteAsync(request);

        _avaliacaoRepositoryMock.Verify(r => r.AtualizarAsync(avaliacao), Times.Once);
    }

    [Fact]
    public async Task ExecutarAsync_RequestInvalido_DeveLancarExcecaoComErrosDeValidacao()
    {
        var userId = Guid.NewGuid();
        SetupHttpContext(userId);

        var request = new EditarAvaliacaoRequest
        {
            TemporadaId = Guid.Empty,
            Nota = 10,
            Comentario = new string('a', 2000),
            RecomendadoPorUsuarioId = Guid.NewGuid(),
            RecomendadoPorNomeLivre = "Nome livre"
        };

        var useCase = CriarUseCase();

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Contains("A temporada é obrigatória.", ex.Message);
        Assert.Contains("A nota deve estar entre 1 e 5.", ex.Message);
        Assert.Contains("Comentário ultrapassa o limite de 1000 caracteres.", ex.Message);
        Assert.Contains("A recomendação deve ser por usuário ou por nome livre, não ambos.", ex.Message);

        _avaliacaoRepositoryMock.Verify(r => r.AtualizarAsync(It.IsAny<Avaliacao>()), Times.Never);
    }

    [Fact]
    public async Task ExecutarAsync_AvaliacaoNaoEncontrada_DeveLancarExcecao()
    {
        var userId = Guid.NewGuid();
        SetupHttpContext(userId);

        var request = new EditarAvaliacaoRequest
        {
            TemporadaId = Guid.NewGuid(),
            Nota = 3
        };

        _avaliacaoRepositoryMock
            .Setup(r => r.ObterPorUsuarioETemporadaAsync(userId, request.TemporadaId))
            .ReturnsAsync((Avaliacao?)null);

        var useCase = CriarUseCase();

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Avaliação não encontrada.", ex.Message);

        _avaliacaoRepositoryMock.Verify(r => r.AtualizarAsync(It.IsAny<Avaliacao>()), Times.Never);
    }

    [Fact]
    public async Task ExecutarAsync_UsuarioNaoAutenticado_DeveLancarExcecao()
    {
        SetupHttpContext(null);

        var request = new EditarAvaliacaoRequest
        {
            TemporadaId = Guid.NewGuid(),
            Nota = 3
        };

        var useCase = CriarUseCase();

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Usuário não autenticado.", ex.Message);

        _avaliacaoRepositoryMock.Verify(r => r.AtualizarAsync(It.IsAny<Avaliacao>()), Times.Never);
    }
}