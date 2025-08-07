using KDramaSystem.Application.UseCases.Dorama.Obter;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using Moq;

public class ObterDoramaUseCaseTests
{
    private readonly Mock<IDoramaRepository> _doramaRepoMock;
    private readonly ObterDoramaUseCase _useCase;

    public ObterDoramaUseCaseTests()
    {
        _doramaRepoMock = new Mock<IDoramaRepository>();
        _useCase = new ObterDoramaUseCase(_doramaRepoMock.Object);
    }

    [Fact]
    public async Task Deve_Obter_Dorama_Com_Sucesso()
    {
        var doramaEsperado = DoramaFactory.CriarDoramaValido();
        _doramaRepoMock.Setup(r => r.ObterPorIdAsync(doramaEsperado.Id))
                       .ReturnsAsync(doramaEsperado);

        var request = new ObterDoramaRequest { Id = doramaEsperado.Id };

        var resultado = await _useCase.ExecutarAsync(request);

        Assert.NotNull(resultado);
        Assert.Equal(doramaEsperado.Id, resultado.Id);
        _doramaRepoMock.Verify(r => r.ObterPorIdAsync(doramaEsperado.Id), Times.Once);
    }

    [Fact]
    public async Task Deve_Lancar_Exception_Quando_Dorama_Nao_Encontrado()
    {
        var idInexistente = Guid.NewGuid();
        _doramaRepoMock.Setup(r => r.ObterPorIdAsync(idInexistente))
                       .ReturnsAsync((Dorama?)null);

        var request = new ObterDoramaRequest { Id = idInexistente };

        var ex = await Assert.ThrowsAsync<Exception>(() => _useCase.ExecutarAsync(request));
        Assert.Equal("Dorama não encontrado.", ex.Message);
        _doramaRepoMock.Verify(r => r.ObterPorIdAsync(idInexistente), Times.Once);
    }
}