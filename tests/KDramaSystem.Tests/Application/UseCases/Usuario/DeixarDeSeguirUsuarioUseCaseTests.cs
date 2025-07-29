using KDramaSystem.Application.Interfaces.Repositories;
using KDramaSystem.Application.UseCases.Usuario.DeixarDeSeguir;
using Moq;

public class DeixarDeSeguirUsuarioUseCaseTests
{
    private readonly Mock<IUsuarioRelacionamentoRepository> _relacionamentoRepository = new();
    private readonly DeixarDeSeguirUsuarioUseCase _useCase;

    public DeixarDeSeguirUsuarioUseCaseTests()
    {
        _useCase = new DeixarDeSeguirUsuarioUseCase(_relacionamentoRepository.Object);
    }

    [Fact]
    public async Task Deve_Deixar_De_Seguir_Com_Sucesso()
    {
        var usuarioLogadoId = Guid.NewGuid();
        var usuarioAlvoId = Guid.NewGuid();

        _relacionamentoRepository.Setup(x => x.RemoverAsync(usuarioLogadoId, usuarioAlvoId))
            .Returns(Task.CompletedTask)
            .Verifiable();

        var resultado = await _useCase.ExecutarAsync(usuarioLogadoId, new DeixarDeSeguirUsuarioRequest(usuarioAlvoId));

        Assert.True(resultado);
        _relacionamentoRepository.Verify(x => x.RemoverAsync(usuarioLogadoId, usuarioAlvoId), Times.Once);
    }
}