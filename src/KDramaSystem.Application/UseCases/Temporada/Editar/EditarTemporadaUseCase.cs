using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Temporada.Editar;

public class EditarTemporadaUseCase
{
    private readonly ITemporadaRepository _temporadaRepository;

    public EditarTemporadaUseCase(ITemporadaRepository temporadaRepository)
    {
        _temporadaRepository = temporadaRepository;
    }

    public async Task ExecutarAsync(EditarTemporadaRequest request)
    {
        var temporada = await _temporadaRepository.ObterPorIdAsync(request.Id);
        if (temporada == null)
            throw new Exception("Temporada não encontrada");

        temporada.AtualizarAnoLancamento(request.AnoLancamento);
        temporada.AtualizarNome(request.Nome);
        temporada.AtualizarSinopse(request.Sinopse);
        if (request.EmExibicao != temporada.EmExibicao)
        {
            if (request.EmExibicao)
                temporada.ReabrirTemporada();
            else
                temporada.MarcarComoEncerrada();
        }
        await _temporadaRepository.AtualizarAsync(temporada);
    }
}