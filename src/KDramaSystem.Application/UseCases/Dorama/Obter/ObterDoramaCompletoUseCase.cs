using KDramaSystem.Application.DTOs.Ator;
using KDramaSystem.Application.DTOs.Dorama;
using KDramaSystem.Application.DTOs.Episodio;
using KDramaSystem.Application.DTOs.Genero;
using KDramaSystem.Domain.Interfaces;

public class ObterDoramaCompletoUseCase
{
    private readonly IDoramaRepository _doramaRepository;
    private readonly ITemporadaRepository _temporadaRepository;
    private readonly IEpisodioRepository _episodioRepository;

    public ObterDoramaCompletoUseCase(
        IDoramaRepository doramaRepository,
        ITemporadaRepository temporadaRepository,
        IEpisodioRepository episodioRepository)
    {
        _doramaRepository = doramaRepository;
        _temporadaRepository = temporadaRepository;
        _episodioRepository = episodioRepository;
    }

    public async Task<DoramaCompletoDto?> ExecutarAsync(Guid doramaId)
    {
        var dorama = await _doramaRepository.ObterPorIdAsync(doramaId);
        if (dorama == null)
            return null;

        var temporadas = await _temporadaRepository.ObterPorDoramaIdAsync(doramaId);

        var temporadasDto = new List<DoramaCompletoDto.TemporadaCompletaDto>();

        foreach (var temporada in temporadas)
        {
            var episodios = await _episodioRepository.ListarPorTemporadaAsync(temporada.Id);

            var episodiosDto = episodios.Select(e => new ObterEpisodioDto
            {
                Id = e.Id,
                TemporadaId = e.TemporadaId,
                Numero = e.Numero,
                Titulo = e.Titulo,
                DuracaoMinutos = e.DuracaoMinutos,
                Tipo = e.Tipo,
                Sinopse = e.Sinopse
            }).ToList();

            temporadasDto.Add(new DoramaCompletoDto.TemporadaCompletaDto
            {
                Id = temporada.Id,
                Nome = temporada.Nome,
                Ordem = temporada.Numero,
                DoramaId = temporada.DoramaId,
                DataEstreia = new DateTime(temporada.AnoLancamento, 1, 1),
                DataFim = temporada.EmExibicao ? (DateTime?)null : DateTime.Now,
                Episodios = episodiosDto
            });
        }

        return new DoramaCompletoDto
        {
            DoramaId = dorama.Id,
            Titulo = dorama.Titulo,
            TituloOriginal = dorama.TituloOriginal,
            Sinopse = dorama.Sinopse,
            CapaUrl = dorama.ImagemCapaUrl,
            AnoLancamento = dorama.AnoLancamento,
            PaisOrigem = dorama.PaisOrigem,
            EmExibicao = dorama.EmExibicao,
            Plataforma = dorama.Plataforma,
            Generos = dorama.Generos.Select(g => new ObterGeneroDto
            {
                Id = g.Id,
                Nome = g.Nome
            }).ToList(),

            Atores = dorama.Atores.Select(da => new ObterAtorDto
            {
                Id = da.Ator.Id,
                Nome = da.Ator.Nome,
                NomeCompleto = da.Ator.NomeCompleto,
                AnoNascimento = da.Ator.AnoNascimento,
                Altura = da.Ator.Altura,
                Pais = da.Ator.Pais,
                Biografia = da.Ator.Biografia,
                FotoUrl = da.Ator.FotoUrl,
                Instagram = da.Ator.Instagram
            }).ToList(),
            Temporadas = temporadasDto
        };
    }
}