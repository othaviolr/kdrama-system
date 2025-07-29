using KDramaSystem.Application.Interfaces.Repositories;
using KDramaSystem.Application.UseCases.Usuario;
using KDramaSystem.Application.UseCases.Usuario.Seguir;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces.Repositories;
using KDramaSystem.Tests.Helpers;
using Moq;

public class SeguirUsuarioUseCaseTests
{
    private readonly Mock<IUsuarioRepository> _usuarioRepository = new();
    private readonly Mock<IUsuarioRelacionamentoRepository> _relacionamentoRepository = new();

    private readonly SeguirUsuarioUseCase _useCase;

    public SeguirUsuarioUseCaseTests()
    {
        _useCase = new SeguirUsuarioUseCase(_usuarioRepository.Object, _relacionamentoRepository.Object);
    }

    [Fact]
    public async Task Deve_Seguir_Usuario_Com_Sucesso()
    {
        var usuarioLogadoId = Guid.NewGuid();
        var usuarioAlvoId = Guid.NewGuid();

        _usuarioRepository.Setup(x => x.ObterPorIdAsync(usuarioAlvoId))
            .ReturnsAsync(UsuarioFactory.CriarUsuario(id: usuarioAlvoId));

        _relacionamentoRepository.Setup(x => x.ExisteRelacionamentoAsync(usuarioLogadoId, usuarioAlvoId))
            .ReturnsAsync(false);

        var resultado = await _useCase.ExecutarAsync(usuarioLogadoId, new SeguirUsuarioRequest(usuarioAlvoId));

        Assert.True(resultado);
        _relacionamentoRepository.Verify(x =>
            x.CriarAsync(It.Is<UsuarioRelacionamento>(r =>
                r.SeguidorId == usuarioLogadoId && r.SeguindoId == usuarioAlvoId)), Times.Once);
    }

    [Fact]
    public async Task Nao_Deve_Permitir_Seguir_A_Si_Mesmo()
    {
        var id = Guid.NewGuid();

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _useCase.ExecutarAsync(id, new SeguirUsuarioRequest(id)));
    }

    [Fact]
    public async Task Deve_Lancar_Erro_Se_Usuario_Alvo_Nao_Existe()
    {
        var usuarioLogadoId = Guid.NewGuid();
        var usuarioAlvoId = Guid.NewGuid();

        _usuarioRepository.Setup(x => x.ObterPorIdAsync(usuarioAlvoId))
            .ReturnsAsync((Usuario?)null);

        await Assert.ThrowsAsync<KeyNotFoundException>(() =>
            _useCase.ExecutarAsync(usuarioLogadoId, new SeguirUsuarioRequest(usuarioAlvoId)));
    }

    [Fact]
    public async Task Deve_Lancar_Erro_Se_Ja_Segue_Usuario()
    {
        var usuarioLogadoId = Guid.NewGuid();
        var usuarioAlvoId = Guid.NewGuid();

        _usuarioRepository.Setup(x => x.ObterPorIdAsync(usuarioAlvoId))
            .ReturnsAsync(UsuarioFactory.CriarUsuario(id: usuarioAlvoId));

        _relacionamentoRepository.Setup(x =>
            x.ExisteRelacionamentoAsync(usuarioLogadoId, usuarioAlvoId))
            .ReturnsAsync(true);

        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _useCase.ExecutarAsync(usuarioLogadoId, new SeguirUsuarioRequest(usuarioAlvoId)));
    }
}