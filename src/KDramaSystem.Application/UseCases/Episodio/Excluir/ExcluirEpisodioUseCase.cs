using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Episodio.Excluir;

public class ExcluirEpisodioUseCase
{
    private readonly IEpisodioRepository _episodioRepository;

    public ExcluirEpisodioUseCase(IEpisodioRepository episodioRepository)
    {
        _episodioRepository = episodioRepository;
    }

    public async Task ExecutarAsync(ExcluirEpisodioRequest request)
    {
        var episodio = await _episodioRepository.ObterPorIdAsync(request.EpisodioId);

        if (episodio == null)
            throw new Exception("Episódio não encontrado.");

        await _episodioRepository.RemoverAsync(request.EpisodioId);
    }
}