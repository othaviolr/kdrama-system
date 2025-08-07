namespace KDramaSystem.Tests.Helpers;

using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Enums;

public static class DoramaCompletoFactory
{
    public static Dorama Criar()
    {
        var doramaId = Guid.NewGuid();
        var usuarioId = Guid.NewGuid();

        var dorama = new Dorama(
            id: doramaId,
            usuarioId: usuarioId,
            titulo: "Meu Dorama Completo",
            paisOrigem: "Coreia do Sul",
            anoLancamento: 2024,
            emExibicao: false,
            plataforma: PlataformaStreaming.Netflix,
            generos: new List<Genero>
            {
                new Genero(Guid.NewGuid(), "Romance"),
                new Genero(Guid.NewGuid(), "Drama")
            },
            imagemCapaUrl: "https://imagem.com/poster.jpg",
            sinopse: "Uma história épica de amor e superação.",
            tituloOriginal: "My Full Drama"
        );

        var ator1 = new Ator(
            id: Guid.NewGuid(),
            nome: "Ji-Soo",
            nomeCompleto: "Kim Ji-Soo",
            anoNascimento: 1995,
            altura: 1.65m,
            pais: "Coreia do Sul",
            biografia: "Atriz premiada.",
            fotoUrl: "https://foto.com/ji-soo.jpg",
            instagram: "@ji.soo"
        );

        var ator2 = new Ator(
            id: Guid.NewGuid(),
            nome: "Min-Ho",
            nomeCompleto: "Lee Min-Ho",
            anoNascimento: 1987,
            altura: 1.87m,
            pais: "Coreia do Sul",
            biografia: "Ator conhecido mundialmente.",
            fotoUrl: "https://foto.com/min-ho.jpg",
            instagram: "@lee.minho"
        );

        var doramaAtor1 = new DoramaAtor(doramaId, ator1.Id);
        var doramaAtor2 = new DoramaAtor(doramaId, ator2.Id);

        dorama.AdicionarAtor(doramaAtor1);
        dorama.AdicionarAtor(doramaAtor2);

        ator1.AdicionarDorama(doramaAtor1);
        ator2.AdicionarDorama(doramaAtor2);

        var temporada = new Temporada(
            id: Guid.NewGuid(),
            doramaId: doramaId,
            numero: 1,
            anoLancamento: 2024,
            emExibicao: false,
            nome: "Temporada 1",
            sinopse: "A temporada que começou tudo."
        );

        var episodio1 = new Episodio(
            id: Guid.NewGuid(),
            temporadaId: temporada.Id,
            numero: 1,
            titulo: "Início",
            duracaoMinutos: 55,
            tipo: TipoEpisodio.Regular,
            sinopse: "Primeiro episódio."
        );

        var episodio2 = new Episodio(
            id: Guid.NewGuid(),
            temporadaId: temporada.Id,
            numero: 2,
            titulo: "Reviravolta",
            duracaoMinutos: 60,
            tipo: TipoEpisodio.Regular,
            sinopse: "As coisas esquentam."
        );

        temporada.AdicionarEpisodio(episodio1);
        temporada.AdicionarEpisodio(episodio2);

        dorama.AdicionarTemporada(temporada);

        return dorama;
    }
}