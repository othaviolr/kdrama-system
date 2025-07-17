using System;
using System.Threading.Tasks;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Infrastructure.Repositories;
using Xunit;

public class UsuarioRepositoryTests
{
    private readonly UsuarioRepository _repository;

    public UsuarioRepositoryTests()
    {
        _repository = new UsuarioRepository();
    }

    [Fact]
    public async Task AdicionarAsync_DeveAdicionarUsuario()
    {
        var usuario = new Usuario(Guid.NewGuid(), "Nome Teste", "nomeusuario", "email@teste.com");

        await _repository.AdicionarAsync(usuario);
        var usuarioObtido = await _repository.ObterPorIdAsync(usuario.Id);

        Assert.NotNull(usuarioObtido);
        Assert.Equal("Nome Teste", usuarioObtido!.Nome);
    }

    [Fact]
    public async Task ObterPorIdAsync_DeveRetornarUsuarioCorreto()
    {
        var usuario1 = new Usuario(Guid.NewGuid(), "Nome 1", "usuario1", "email1@teste.com");
        var usuario2 = new Usuario(Guid.NewGuid(), "Nome 2", "usuario2", "email2@teste.com");

        await _repository.AdicionarAsync(usuario1);
        await _repository.AdicionarAsync(usuario2);

        var resultado = await _repository.ObterPorIdAsync(usuario2.Id);

        Assert.NotNull(resultado);
        Assert.Equal("usuario2", resultado!.NomeUsuario);
    }

    [Fact]
    public async Task NomeUsuarioExisteAsync_RetornaTrueSeExistir()
    {
        var usuario = new Usuario(Guid.NewGuid(), "Nome Teste", "usuarioexiste", "email@teste.com");
        await _repository.AdicionarAsync(usuario);

        var existe = await _repository.NomeUsuarioExisteAsync("usuarioexiste");

        Assert.True(existe);
    }

    [Fact]
    public async Task NomeUsuarioExisteAsync_RetornaFalseSeNaoExistir()
    {
        var existe = await _repository.NomeUsuarioExisteAsync("usuarioinexistente");

        Assert.False(existe);
    }
}