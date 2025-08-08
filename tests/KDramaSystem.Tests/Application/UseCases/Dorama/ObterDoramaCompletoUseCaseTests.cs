using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Enums;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Tests.Helpers;
using Moq;

public class ObterDoramaCompletoUseCaseTests
{
    private readonly Mock<IDoramaRepository> _doramaRepoMock = new();
    private readonly Mock<ITemporadaRepository> _temporadaRepoMock = new();
    private readonly Mock<IEpisodioRepository> _episodioRepoMock = new();

    private readonly ObterDoramaCompletoUseCase _useCase;

    public ObterDoramaCompletoUseCaseTests()
    {
        _useCase = new ObterDoramaCompletoUseCase(
            _doramaRepoMock.Object,
            _temporadaRepoMock.Object,
            _episodioRepoMock.Object
        );
    }

    [Fact]
    public async Task Deve_RetornarNull_QuandoDoramaNaoExiste()
    {
        var doramaId = Guid.NewGuid();
        _doramaRepoMock.Setup(repo => repo.ObterPorIdAsync(doramaId))
                       .ReturnsAsync((Dorama?)null);

        var resultado = await _useCase.ExecutarAsync(doramaId);

        Assert.Null(resultado);
    }

    [Fact]
    public async Task Deve_RetornarDoramaCompleto_SemTemporadas()
    {
        var dorama = DoramaCompletoFactory.Criar();
        _doramaRepoMock.Setup(r => r.ObterPorIdAsync(dorama.Id)).ReturnsAsync(dorama);
        _temporadaRepoMock.Setup(r => r.ObterPorDoramaIdAsync(dorama.Id)).ReturnsAsync(new List<Temporada>());

        var resultado = await _useCase.ExecutarAsync(dorama.Id);

        Assert.NotNull(resultado);
        Assert.Equal(dorama.Titulo, resultado!.Titulo);
        Assert.Empty(resultado.Temporadas);
    }

    [Fact]
    public async Task Deve_RetornarDoramaComTemporadas_SemEpisodios()
    {
        var dorama = DoramaCompletoFactory.Criar();
        var temporada = new Temporada(
            id: Guid.NewGuid(),
            doramaId: dorama.Id,
            numero: 1,
            anoLancamento: 2022,
            emExibicao: false,
            nome: "Temp 1"
        );

        _doramaRepoMock.Setup(r => r.ObterPorIdAsync(dorama.Id)).ReturnsAsync(dorama);
        _temporadaRepoMock.Setup(r => r.ObterPorDoramaIdAsync(dorama.Id)).ReturnsAsync(new List<Temporada> { temporada });
        _episodioRepoMock.Setup(r => r.ListarPorTemporadaAsync(temporada.Id)).ReturnsAsync(new List<Episodio>());

        var resultado = await _useCase.ExecutarAsync(dorama.Id);

        Assert.NotNull(resultado);
        Assert.Single(resultado!.Temporadas);
        Assert.Empty(resultado.Temporadas[0].Episodios);
    }

    [Fact]
    public async Task Deve_RetornarDoramaComTemporadasEEpisodios()
    {
        var dorama = DoramaCompletoFactory.Criar();
        var temporada = new Temporada(
            id: Guid.NewGuid(),
            doramaId: dorama.Id,
            numero: 1,
            anoLancamento: 2022,
            emExibicao: false,
            nome: "Temp 1"
        );

        var episodio = new Episodio(
            id: Guid.NewGuid(),
            temporadaId: temporada.Id,
            numero: 1,
            titulo: "Ep 1",
            duracaoMinutos: 45,
            tipo: TipoEpisodio.Regular,
            sinopse: "Resumo"
        );

        _doramaRepoMock.Setup(r => r.ObterPorIdAsync(dorama.Id)).ReturnsAsync(dorama);
        _temporadaRepoMock.Setup(r => r.ObterPorDoramaIdAsync(dorama.Id)).ReturnsAsync(new List<Temporada> { temporada });
        _episodioRepoMock.Setup(r => r.ListarPorTemporadaAsync(temporada.Id)).ReturnsAsync(new List<Episodio> { episodio });

        var resultado = await _useCase.ExecutarAsync(dorama.Id);

        Assert.NotNull(resultado);
        Assert.Single(resultado!.Temporadas);
        Assert.Single(resultado.Temporadas[0].Episodios);
        Assert.Equal("Ep 1", resultado.Temporadas[0].Episodios[0].Titulo);
    }
}