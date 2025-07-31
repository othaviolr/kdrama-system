using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Temporada.Obter;

public class ObterTemporadaPorIdUseCase
{
    private readonly ITemporadaRepository _temporadaRepository;

    public ObterTemporadaPorIdUseCase(ITemporadaRepository temporadaRepository)
    {
        _temporadaRepository = temporadaRepository;
    }

    public async Task<KDramaSystem.Domain.Entities.Temporada?> ExecutarAsync(ObterTemporadaPorIdRequest request)
    {
        return await _temporadaRepository.ObterPorIdAsync(request.Id);
    }
}