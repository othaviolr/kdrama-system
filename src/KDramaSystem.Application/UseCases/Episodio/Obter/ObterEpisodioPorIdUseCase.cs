using KDramaSystem.Application.DTOs.Episodio;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Episodio.Obter;

public class ObterEpisodioPorIdUseCase
{
    private readonly IEpisodioRepository _episodioRepository;

    public ObterEpisodioPorIdUseCase(IEpisodioRepository episodioRepository)
    {
        _episodioRepository = episodioRepository;
    }

    public async Task<ObterEpisodioDto?> ExecutarAsync(Guid episodioId)
    {
        var episodio = await _episodioRepository.ObterPorIdAsync(episodioId);
        if (episodio == null) return null;

        var dto = new ObterEpisodioDto
        {
            Id = episodio.Id,
            TemporadaId = episodio.TemporadaId,
            Numero = episodio.Numero,
            Titulo = episodio.Titulo,
            DuracaoMinutos = episodio.DuracaoMinutos,
            Tipo = episodio.Tipo,
            Sinopse = episodio.Sinopse
        };

        return dto;
    }
}