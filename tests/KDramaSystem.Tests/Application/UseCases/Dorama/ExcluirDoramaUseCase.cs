using KDramaSystem.Application.UseCases.Dorama.Excluir;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using Moq;

public class ExcluirDoramaUseCaseTests
{
    private readonly Guid _doramaId = Guid.NewGuid();
    private readonly Guid _usuarioId = Guid.NewGuid();

    private Dorama CriarDoramaParaTeste(Guid usuarioId)
    {
        return new Dorama(
            _doramaId,
            usuarioId,
            "Título Teste",
            "Coreia do Sul",
            2023,
            true,
            KDramaSystem.Domain.Enums.PlataformaStreaming.Netflix,
            new List<KDramaSystem.Domain.Entities.Genero> { new KDramaSystem.Domain.Entities.Genero(Guid.NewGuid(), "Romance") },
            "https://teste.com/capa.jpg"
        );
    }

    [Fact]
    public async Task Deve_Excluir_Dorama_Com_Sucesso()
    {
        var dorama = CriarDoramaParaTeste(_usuarioId);

        var doramaRepoMock = new Mock<IDoramaRepository>();
        doramaRepoMock.Setup(r => r.ObterPorIdAsync(_doramaId))
                      .ReturnsAsync(dorama);

        doramaRepoMock.Setup(r => r.ExcluirAsync(_doramaId))
                      .Returns(Task.CompletedTask)
                      .Verifiable();

        var useCase = new ExcluirDoramaUseCase(doramaRepoMock.Object);

        var request = new ExcluirDoramaRequest
        {
            Id = _doramaId,
            UsuarioId = _usuarioId
        };

        await useCase.ExecutarAsync(request);

        doramaRepoMock.Verify(r => r.ExcluirAsync(_doramaId), Times.Once);
    }

    [Fact]
    public async Task Deve_Falhar_Quando_Dorama_Nao_Encontrado()
    {
        var doramaRepoMock = new Mock<IDoramaRepository>();
        doramaRepoMock.Setup(r => r.ObterPorIdAsync(_doramaId))
                      .ReturnsAsync((Dorama?)null);

        var useCase = new ExcluirDoramaUseCase(doramaRepoMock.Object);

        var request = new ExcluirDoramaRequest
        {
            Id = _doramaId,
            UsuarioId = _usuarioId
        };

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecutarAsync(request));
        Assert.Equal("Dorama não encontrado.", ex.Message);
    }

    [Fact]
    public async Task Deve_Falhar_Quando_Usuario_Nao_Dono_Do_Dorama()
    {
        var dorama = CriarDoramaParaTeste(Guid.NewGuid());

        var doramaRepoMock = new Mock<IDoramaRepository>();
        doramaRepoMock.Setup(r => r.ObterPorIdAsync(_doramaId))
                      .ReturnsAsync(dorama);

        var useCase = new ExcluirDoramaUseCase(doramaRepoMock.Object);

        var request = new ExcluirDoramaRequest
        {
            Id = _doramaId,
            UsuarioId = _usuarioId
        };

        var ex = await Assert.ThrowsAsync<UnauthorizedAccessException>(() => useCase.ExecutarAsync(request));
        Assert.Equal("Acesso negado.", ex.Message);
    }
}