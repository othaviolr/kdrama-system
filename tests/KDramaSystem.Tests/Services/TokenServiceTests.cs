using KDramaSystem.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Xunit;
using System;
using System.Collections.Generic;

public class TokenServiceTests
{
    private readonly TokenService _service;

    public TokenServiceTests()
    {
        var inMemorySettings = new Dictionary<string, string> {
            {"Jwt:Key", "MinhaChaveSecretaSuperSegura1234567890"},
            {"Jwt:Issuer", "KDramaSystemAPI"},
            {"Jwt:Audience", "KDramaSystemAPIUsers"}
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        _service = new TokenService(configuration);
    }

    [Fact]
    public void GerarToken_Deve_Retornar_TokenNaoNulo()
    {
        var usuarioId = Guid.NewGuid();
        var nomeUsuario = "othaviolr";
        var email = "othavio@gmail.com";

        var token = _service.GerarToken(usuarioId, nomeUsuario, email);

        Assert.False(string.IsNullOrEmpty(token));
    }
}