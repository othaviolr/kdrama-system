using KDramaSystem.Application.UseCases.Dorama.Criar;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Enums;

public static class DoramaFactory
{
    public static Dorama CriarDoramaValido(
        Guid? id = null,
        Guid? usuarioId = null,
        string? titulo = null,
        string? paisOrigem = null,
        int? anoLancamento = null,
        bool? emExibicao = null,
        PlataformaStreaming? plataforma = null,
        List<Genero>? generos = null,
        string? imagemCapaUrl = null,
        string? sinopse = null,
        string? tituloOriginal = null
    )
    {
        return new Dorama(
            id ?? Guid.NewGuid(),
            usuarioId ?? Guid.NewGuid(),
            titulo ?? "Dorama Teste",
            paisOrigem ?? "Coreia do Sul",
            anoLancamento ?? DateTime.Now.Year,
            emExibicao ?? true,
            plataforma ?? PlataformaStreaming.Netflix,
            generos ?? new List<Genero> { new Genero(Guid.NewGuid(), "Romance") },
            imagemCapaUrl ?? "https://teste.com/imagem.jpg",
            sinopse ?? "Sinopse do dorama para teste.",
            tituloOriginal ?? "Título Original"
        );
    }

    public static CriarDoramaRequest CriarRequestValido(
        Guid? usuarioCriadorId = null,
        string? titulo = null,
        string? tituloOriginal = null,
        string? paisOrigem = null,
        int? anoLancamento = null,
        bool? emExibicao = null,
        int? plataforma = null,
        List<Guid>? generoIds = null,
        string? sinopse = null,
        List<Guid>? atorIds = null,
        string? imagemCapaUrl = null
    )
    {
        return new CriarDoramaRequest
        {
            UsuarioCriadorId = usuarioCriadorId ?? Guid.NewGuid(),
            Titulo = titulo ?? "Dorama Teste",
            TituloOriginal = tituloOriginal ?? "Original Title",
            PaisOrigem = paisOrigem ?? "Coreia do Sul",
            AnoLancamento = anoLancamento ?? DateTime.Now.Year,
            EmExibicao = emExibicao ?? true,
            Plataforma = plataforma ?? 0,
            GeneroIds = generoIds ?? new List<Guid> { Guid.NewGuid() },
            Sinopse = sinopse ?? "Sinopse do dorama para teste.",
            AtorIds = atorIds ?? new List<Guid> { Guid.NewGuid() },
            ImagemCapaUrl = imagemCapaUrl ?? "https://teste.com/imagem.jpg"
        };
    }
}