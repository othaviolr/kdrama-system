using KDramaSystem.Application.Exceptions;
using KDramaSystem.Application.UseCases.Dorama;
using KDramaSystem.Application.UseCases.Dorama.Editar;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Enums;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.Interfaces.Repositories;
using Moq;

public class EditarDoramaUseCaseTests
{
    private EditarDoramaRequest CriarRequestValido()
    {
        return new EditarDoramaRequest
        {
            DoramaId = Guid.NewGuid(),
            UsuarioEditorId = Guid.NewGuid(),
            Titulo = "Novo Título",
            TituloOriginal = "Original Title",
            PaisOrigem = "Coreia do Sul",
            AnoLancamento = 2023,
            EmExibicao = true,
            Plataforma = (int)PlataformaStreaming.Netflix,
            GeneroIds = new List<Guid> { Guid.NewGuid() },
            ImagemCapaUrl = "https://teste.com/imagem.jpg",
            Sinopse = "Nova sinopse."
        };
    }

    private Usuario CriarUsuario(Guid id)
        => new Usuario(id, "Usuário Teste", "usuarioTeste", "usuario@teste.com");

    private List<Genero> CriarGeneros(List<Guid> ids)
    {
        var generos = new List<Genero>();
        foreach (var id in ids)
            generos.Add(new Genero(id, "Genero Teste"));
        return generos;
    }

    private Dorama CriarDorama(Guid id, Guid usuarioId, List<Genero> generos)
        => new Dorama(
            id,
            usuarioId,
            "Título Antigo",
            "Coreia do Sul",
            2020,
            true,
            PlataformaStreaming.Netflix,
            generos,
            "https://teste.com/antiga.jpg",
            "Sinopse antiga"
        );

    [Fact]
    public async Task Deve_Editar_Dorama_Com_Sucesso()
    {
        var request = CriarRequestValido();
        var generos = CriarGeneros(request.GeneroIds!);
        var dorama = CriarDorama(request.DoramaId, request.UsuarioEditorId, generos);

        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        usuarioRepoMock.Setup(r => r.ObterPorIdAsync(request.UsuarioEditorId))
                       .ReturnsAsync(CriarUsuario(request.UsuarioEditorId));

        var doramaRepoMock = new Mock<IDoramaRepository>();
        doramaRepoMock.Setup(r => r.ObterPorIdAsync(request.DoramaId))
                      .ReturnsAsync(dorama);

        var generoRepoMock = new Mock<IGeneroRepository>();
        generoRepoMock.Setup(r => r.ObterPorIdsAsync(request.GeneroIds!))
                      .ReturnsAsync(generos);

        var useCase = new EditarDoramaUseCase(
            doramaRepoMock.Object,
            usuarioRepoMock.Object,
            generoRepoMock.Object
        );

        await useCase.ExecutarAsync(request);

        doramaRepoMock.Verify(r => r.AtualizarAsync(It.IsAny<Dorama>()), Times.Once);

        // Pode fazer asserts específicos aqui se quiser, tipo:
        Assert.Equal(request.Titulo, dorama.Titulo);
        Assert.Equal(request.PaisOrigem, dorama.PaisOrigem);
        Assert.Equal(request.AnoLancamento, dorama.AnoLancamento);
    }

    [Fact]
    public async Task Deve_Falhar_Quando_UsuarioNaoEncontrado()
    {
        var request = CriarRequestValido();

        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        usuarioRepoMock.Setup(r => r.ObterPorIdAsync(request.UsuarioEditorId))
                       .ReturnsAsync((Usuario?)null);

        var doramaRepoMock = new Mock<IDoramaRepository>();
        var generoRepoMock = new Mock<IGeneroRepository>();

        var useCase = new EditarDoramaUseCase(
            doramaRepoMock.Object,
            usuarioRepoMock.Object,
            generoRepoMock.Object
        );

        var ex = await Assert.ThrowsAsync<NotFoundException>(() => useCase.ExecutarAsync(request));
        Assert.Equal("Usuário não encontrado.", ex.Message);
    }

    [Fact]
    public async Task Deve_Falhar_Quando_DoramaNaoEncontrado()
    {
        var request = CriarRequestValido();

        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        usuarioRepoMock.Setup(r => r.ObterPorIdAsync(request.UsuarioEditorId))
                       .ReturnsAsync(CriarUsuario(request.UsuarioEditorId));

        var doramaRepoMock = new Mock<IDoramaRepository>();
        doramaRepoMock.Setup(r => r.ObterPorIdAsync(request.DoramaId))
                      .ReturnsAsync((Dorama?)null);

        var generoRepoMock = new Mock<IGeneroRepository>();

        var useCase = new EditarDoramaUseCase(
            doramaRepoMock.Object,
            usuarioRepoMock.Object,
            generoRepoMock.Object
        );

        var ex = await Assert.ThrowsAsync<NotFoundException>(() => useCase.ExecutarAsync(request));
        Assert.Equal("Dorama não encontrado.", ex.Message);
    }

    [Fact]
    public async Task Deve_Falhar_Quando_UsuarioNaoForDonoDoDorama()
    {
        var request = CriarRequestValido();
        var generos = CriarGeneros(request.GeneroIds!);
        var dorama = CriarDorama(request.DoramaId, Guid.NewGuid(), generos); // dono diferente

        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        usuarioRepoMock.Setup(r => r.ObterPorIdAsync(request.UsuarioEditorId))
                       .ReturnsAsync(CriarUsuario(request.UsuarioEditorId));

        var doramaRepoMock = new Mock<IDoramaRepository>();
        doramaRepoMock.Setup(r => r.ObterPorIdAsync(request.DoramaId))
                      .ReturnsAsync(dorama);

        var generoRepoMock = new Mock<IGeneroRepository>();

        var useCase = new EditarDoramaUseCase(
            doramaRepoMock.Object,
            usuarioRepoMock.Object,
            generoRepoMock.Object
        );

        var ex = await Assert.ThrowsAsync<UnauthorizedAccessException>(() => useCase.ExecutarAsync(request));
        Assert.Equal("Acesso negado.", ex.Message);
    }

    [Fact]
    public async Task Deve_Falhar_Quando_GenerosInformadosSaoInvalidos()
    {
        var request = CriarRequestValido();

        var usuarioRepoMock = new Mock<IUsuarioRepository>();
        usuarioRepoMock.Setup(r => r.ObterPorIdAsync(request.UsuarioEditorId))
                       .ReturnsAsync(CriarUsuario(request.UsuarioEditorId));

        var doramaRepoMock = new Mock<IDoramaRepository>();
        doramaRepoMock.Setup(r => r.ObterPorIdAsync(request.DoramaId))
                      .ReturnsAsync(CriarDorama(request.DoramaId, request.UsuarioEditorId, CriarGeneros(request.GeneroIds!)));

        var generoRepoMock = new Mock<IGeneroRepository>();
        generoRepoMock.Setup(r => r.ObterPorIdsAsync(request.GeneroIds!))
                      .ReturnsAsync(new List<Genero>()); // Retorna lista vazia = inválido

        var useCase = new EditarDoramaUseCase(
            doramaRepoMock.Object,
            usuarioRepoMock.Object,
            generoRepoMock.Object
        );

        var ex = await Assert.ThrowsAsync<Exception>(() => useCase.ExecutarAsync(request));
        Assert.Equal("Um ou mais gêneros informados são inválidos.", ex.Message);
    }
}
