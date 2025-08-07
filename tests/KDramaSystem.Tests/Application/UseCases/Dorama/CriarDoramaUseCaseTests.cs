using KDramaSystem.Application.UseCases.Dorama;
using KDramaSystem.Application.UseCases.Dorama.Criar;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.Interfaces.Repositories;
using Moq;

public class CriarDoramaUseCaseTests
{
    private CriarDoramaRequest CriarRequestValido() => DoramaFactory.CriarRequestValido();

    private Usuario CriarUsuario(Guid id) => new Usuario(id, "Usuário Teste", "usuarioTeste", "usuario@teste.com");

    private List<Genero> CriarGeneros(List<Guid> ids)
    {
        var generos = new List<Genero>();
        foreach (var id in ids)
            generos.Add(new Genero(id, "Genero Teste"));
        return generos;
    }

    private List<Ator> CriarAtores(List<Guid> ids)
    {
        var atores = new List<Ator>();
        foreach (var id in ids)
            atores.Add(new Ator(id, "Ator Teste"));
        return atores;
    }

    [Fact]
    public async Task Deve_Criar_Dorama_Com_Sucesso()
    {
        var request = CriarRequestValido();

        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        usuarioRepoMock.Setup(r => r.ObterPorIdAsync(request.UsuarioCriadorId))
                       .ReturnsAsync(CriarUsuario(request.UsuarioCriadorId));

        var generoRepoMock = new Mock<IGeneroRepository>();
        generoRepoMock.Setup(r => r.ObterPorIdsAsync(request.GeneroIds))
                      .ReturnsAsync(CriarGeneros(request.GeneroIds));

        var atorRepoMock = new Mock<IAtorRepository>();
        atorRepoMock.Setup(r => r.ObterPorIdsAsync(request.AtorIds!))
                    .ReturnsAsync(CriarAtores(request.AtorIds!));

        var doramaRepoMock = new Mock<IDoramaRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();
        unitOfWorkMock.Setup(u => u.SalvarAlteracoesAsync())
                      .Returns(Task.CompletedTask);

        var useCase = new CriarDoramaUseCase(
            doramaRepoMock.Object,
            unitOfWorkMock.Object,
            usuarioRepoMock.Object,
            generoRepoMock.Object,
            atorRepoMock.Object
        );

        var resultado = await useCase.ExecutarAsync(request);

        doramaRepoMock.Verify(r => r.AdicionarAsync(It.IsAny<Dorama>()), Times.Once);
        unitOfWorkMock.Verify(u => u.SalvarAlteracoesAsync(), Times.Once);
        Assert.NotEqual(Guid.Empty, resultado);
    }

    [Fact]
    public async Task Deve_Falhar_Quando_UsuarioNaoEncontrado()
    {
        var request = CriarRequestValido();

        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        usuarioRepoMock.Setup(r => r.ObterPorIdAsync(request.UsuarioCriadorId))
                       .ReturnsAsync((Usuario?)null);

        var generoRepoMock = new Mock<IGeneroRepository>();
        var atorRepoMock = new Mock<IAtorRepository>();
        var doramaRepoMock = new Mock<IDoramaRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var useCase = new CriarDoramaUseCase(
            doramaRepoMock.Object,
            unitOfWorkMock.Object,
            usuarioRepoMock.Object,
            generoRepoMock.Object,
            atorRepoMock.Object
        );

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecutarAsync(request));
        Assert.Equal("Usuário não encontrado.", ex.Message);
    }

    [Fact]
    public async Task Deve_Falhar_Quando_GeneroIds_NullOuVazio()
    {
        var request = DoramaFactory.CriarRequestValido(generoIds: null);

        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        usuarioRepoMock.Setup(r => r.ObterPorIdAsync(request.UsuarioCriadorId))
                       .ReturnsAsync(CriarUsuario(request.UsuarioCriadorId));

        var generoRepoMock = new Mock<IGeneroRepository>();
        var atorRepoMock = new Mock<IAtorRepository>();
        var doramaRepoMock = new Mock<IDoramaRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var useCase = new CriarDoramaUseCase(
            doramaRepoMock.Object,
            unitOfWorkMock.Object,
            usuarioRepoMock.Object,
            generoRepoMock.Object,
            atorRepoMock.Object
        );

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecutarAsync(request));
        Assert.Equal("É necessário informar pelo menos um gênero.", ex.Message);
    }

    [Fact]
    public async Task Deve_Falhar_Quando_AtorIds_NullOuVazio()
    {
        var request = DoramaFactory.CriarRequestValido(atorIds: null);

        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        usuarioRepoMock.Setup(r => r.ObterPorIdAsync(request.UsuarioCriadorId))
                       .ReturnsAsync(CriarUsuario(request.UsuarioCriadorId));

        var generoRepoMock = new Mock<IGeneroRepository>();
        var atorRepoMock = new Mock<IAtorRepository>();
        var doramaRepoMock = new Mock<IDoramaRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var useCase = new CriarDoramaUseCase(
            doramaRepoMock.Object,
            unitOfWorkMock.Object,
            usuarioRepoMock.Object,
            generoRepoMock.Object,
            atorRepoMock.Object
        );

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecutarAsync(request));
        Assert.Equal("É necessário informar pelo menos um ator.", ex.Message);
    }

    [Fact]
    public async Task Deve_Falhar_Quando_Generos_Informados_Sao_Invalidos()
    {
        var request = CriarRequestValido();

        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        usuarioRepoMock.Setup(r => r.ObterPorIdAsync(request.UsuarioCriadorId))
                       .ReturnsAsync(CriarUsuario(request.UsuarioCriadorId));

        var generoRepoMock = new Mock<IGeneroRepository>();
        generoRepoMock.Setup(r => r.ObterPorIdsAsync(request.GeneroIds))
                      .ReturnsAsync(new List<Genero>());

        var atorRepoMock = new Mock<IAtorRepository>();
        var doramaRepoMock = new Mock<IDoramaRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var useCase = new CriarDoramaUseCase(
            doramaRepoMock.Object,
            unitOfWorkMock.Object,
            usuarioRepoMock.Object,
            generoRepoMock.Object,
            atorRepoMock.Object
        );

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecutarAsync(request));
        Assert.Equal("Um ou mais gêneros informados são inválidos.", ex.Message);
    }

    [Fact]
    public async Task Deve_Falhar_Quando_Atores_Informados_Sao_Invalidos()
    {
        var request = CriarRequestValido();

        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        usuarioRepoMock.Setup(r => r.ObterPorIdAsync(request.UsuarioCriadorId))
                       .ReturnsAsync(CriarUsuario(request.UsuarioCriadorId));

        var generoRepoMock = new Mock<IGeneroRepository>();
        generoRepoMock.Setup(r => r.ObterPorIdsAsync(request.GeneroIds))
                      .ReturnsAsync(CriarGeneros(request.GeneroIds));

        var atorRepoMock = new Mock<IAtorRepository>();
        atorRepoMock.Setup(r => r.ObterPorIdsAsync(request.AtorIds!))
                    .ReturnsAsync(new List<Ator>());

        var doramaRepoMock = new Mock<IDoramaRepository>();
        var unitOfWorkMock = new Mock<IUnitOfWork>();

        var useCase = new CriarDoramaUseCase(
            doramaRepoMock.Object,
            unitOfWorkMock.Object,
            usuarioRepoMock.Object,
            generoRepoMock.Object,
            atorRepoMock.Object
        );

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecutarAsync(request));
        Assert.Equal("Um ou mais atores informados são inválidos.", ex.Message);
    }
}
