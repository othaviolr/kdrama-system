using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Episodio.Criar;

public class CriarEpisodioUseCase
{
    private readonly IEpisodioRepository _episodioRepository;

    public CriarEpisodioUseCase(IEpisodioRepository episodioRepository)
    {
        _episodioRepository = episodioRepository;
    }

    public async Task<Guid> ExecutarAsync(CriarEpisodioRequest request)
    {
        var episodiosNaTemporada = await _episodioRepository.ListarPorTemporadaAsync(request.TemporadaId);
        if (episodiosNaTemporada.Any(e => e.Numero == request.Numero))
            throw new Exception($"Já existe um episódio com o número {request.Numero} nessa temporada.");

        var episodio = new KDramaSystem.Domain.Entities.Episodio(
            Guid.NewGuid(),
            request.TemporadaId,
            request.Numero,
            request.Titulo,
            request.DuracaoMinutos,
            request.Tipo,
            request.Sinopse
        );

        await _episodioRepository.AdicionarAsync(episodio);

        return episodio.Id;
    }
}