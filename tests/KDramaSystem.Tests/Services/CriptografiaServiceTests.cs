using KDramaSystem.Infrastructure.Services;
using Xunit;

public class CriptografiaServiceTests
{
    private readonly CriptografiaService _service;

    public CriptografiaServiceTests()
    {
        _service = new CriptografiaService();
    }

    [Fact]
    public void GerarHash_Deve_GerarHashDiferenteDaSenhaOriginal()
    {
        var senha = "minhaSenha123";
        var hash = _service.GerarHash(senha);

        Assert.NotNull(hash);
        Assert.NotEqual(senha, hash);
    }

    [Fact]
    public void VerificarSenha_Deve_RetornarTrue_QuandoSenhaCorreta()
    {
        var senha = "minhaSenha123";
        var hash = _service.GerarHash(senha);

        var resultado = _service.VerificarSenha(senha, hash);

        Assert.True(resultado);
    }

    [Fact]
    public void VerificarSenha_Deve_RetornarFalse_QuandoSenhaIncorreta()
    {
        var senha = "minhaSenha123";
        var hash = _service.GerarHash(senha);

        var resultado = _service.VerificarSenha("senhaErrada", hash);

        Assert.False(resultado);
    }
}