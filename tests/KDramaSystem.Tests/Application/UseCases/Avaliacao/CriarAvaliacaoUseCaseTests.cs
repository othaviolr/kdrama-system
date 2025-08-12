using System.Security.Claims;
using KDramaSystem.Application.UseCases.Avaliacao.Criar;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Moq;

public class CriarAvaliacaoUseCaseTests
{
    private readonly Mock<IAvaliacaoRepository> _avaliacaoRepositoryMock = new();
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock = new();
    private readonly CriarAvaliacaoValidator _validator = new();

    private CriarAvaliacaoUseCase CriarUseCase()
    {
        return new CriarAvaliacaoUseCase(_avaliacaoRepositoryMock.Object, _httpContextAccessorMock.Object, _validator);
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
    public async Task ExecutarAsync_QuandoRequestValido_DeveAdicionarAvaliacao()
    {
        var userId = Guid.NewGuid();
        SetupHttpContext(userId);

        var request = new CriarAvaliacaoRequest
        {
            TemporadaId = Guid.NewGuid(),
            Nota = 4,
            Comentario = "Comentário teste",
            RecomendadoPorUsuarioId = null,
            RecomendadoPorNomeLivre = "Indicação Teste"
        };

        _avaliacaoRepositoryMock
            .Setup(r => r.ExisteAvaliacaoAsync(userId, request.TemporadaId))
            .ReturnsAsync(false);

        var useCase = CriarUseCase();

        await useCase.ExecuteAsync(request);

        _avaliacaoRepositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Avaliacao>()), Times.Once);
    }

    [Fact]
    public async Task ExecutarAsync_QuandoRequestInvalido_DeveLancarExcecao()
    {
        var userId = Guid.NewGuid();
        SetupHttpContext(userId);

        var request = new CriarAvaliacaoRequest
        {
            TemporadaId = Guid.Empty, 
            Nota = 10,                
            Comentario = new string('a', 2000), 
            RecomendadoPorUsuarioId = Guid.NewGuid(),
            RecomendadoPorNomeLivre = "Nome livre"
        };

        var useCase = CriarUseCase();

        var exception = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));

        Assert.Contains("A temporada é obrigatória.", exception.Message);
        Assert.Contains("A nota deve estar entre 1 e 5.", exception.Message);
        Assert.Contains("Comentário ultrapassa o limite de 1000 caracteres.", exception.Message);
        Assert.Contains("A recomendação deve ser por usuário ou por nome livre, não ambos.", exception.Message);

        _avaliacaoRepositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Avaliacao>()), Times.Never);
    }

    [Fact]
    public async Task ExecutarAsync_QuandoUsuarioNaoAutenticado_DeveLancarExcecao()
    {
        SetupHttpContext(null);

        var request = new CriarAvaliacaoRequest
        {
            TemporadaId = Guid.NewGuid(),
            Nota = 3,
        };

        var useCase = CriarUseCase();

        var exception = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Usuário não autenticado.", exception.Message);

        _avaliacaoRepositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Avaliacao>()), Times.Never);
    }

    [Fact]
    public async Task ExecutarAsync_QuandoAvaliacaoJaExiste_DeveLancarExcecao()
    {
        var userId = Guid.NewGuid();
        SetupHttpContext(userId);

        var request = new CriarAvaliacaoRequest
        {
            TemporadaId = Guid.NewGuid(),
            Nota = 5,
        };

        _avaliacaoRepositoryMock
            .Setup(r => r.ExisteAvaliacaoAsync(userId, request.TemporadaId))
            .ReturnsAsync(true);

        var useCase = CriarUseCase();

        var exception = await Assert.ThrowsAsync<Exception>(() => useCase.ExecuteAsync(request));
        Assert.Equal("Você já avaliou esta temporada.", exception.Message);

        _avaliacaoRepositoryMock.Verify(r => r.AdicionarAsync(It.IsAny<Avaliacao>()), Times.Never);
    }
}