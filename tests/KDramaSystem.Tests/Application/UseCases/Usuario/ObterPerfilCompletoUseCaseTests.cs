using KDramaSystem.Application.UseCases.Usuario.ObterPerfilCompleto;
using KDramaSystem.Application.UseCases.Usuario.Dtos;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces.Repositories;
using KDramaSystem.Application.Interfaces;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

public class ObterPerfilCompletoUseCaseTests
{
    private readonly Mock<IUsuarioRepository> _usuarioRepoMock = new();
    private readonly Mock<IUsuarioAutenticadoProvider> _usuarioAutenticadoMock = new();

    private readonly ObterPerfilCompletoUseCase _useCase;

    public ObterPerfilCompletoUseCaseTests()
    {
        _useCase = new ObterPerfilCompletoUseCase(_usuarioRepoMock.Object, _usuarioAutenticadoMock.Object);
    }

    [Fact]
    public async Task ExecutarAsync_DeveRetornarPerfilQuandoUsuarioExiste()
    {
        var userId = Guid.NewGuid();
        var usuario = new Usuario(userId, "Othavio", "othaviolr", "othavio@gmail.com");

        _usuarioAutenticadoMock.Setup(x => x.ObterUsuarioId()).Returns(userId);
        _usuarioRepoMock.Setup(x => x.ObterPorIdAsync(userId)).ReturnsAsync(usuario);

        var resultado = await _useCase.ExecutarAsync(new ObterPerfilCompletoRequest());

        Assert.NotNull(resultado);
        Assert.Equal("Othavio", resultado!.Nome);
        Assert.Equal("othaviolr", resultado.NomeUsuario);
        Assert.Equal("othavio@gmail.com", resultado.Email);
    }

    [Fact]
    public async Task ExecutarAsync_DeveRetornarNullQuandoUsuarioNaoExiste()
    {
        var userId = Guid.NewGuid();

        _usuarioAutenticadoMock.Setup(x => x.ObterUsuarioId()).Returns(userId);
        _usuarioRepoMock.Setup(x => x.ObterPorIdAsync(userId)).ReturnsAsync((Usuario?)null);

        var resultado = await _useCase.ExecutarAsync(new ObterPerfilCompletoRequest());

        Assert.Null(resultado);
    }
}