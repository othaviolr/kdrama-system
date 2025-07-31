using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Temporada.Excluir;

public class ExcluirTemporadaUseCase
{
    private readonly ITemporadaRepository _temporadaRepository;

    public ExcluirTemporadaUseCase(ITemporadaRepository temporadaRepository)
    {
        _temporadaRepository = temporadaRepository;
    }

    public async Task ExecutarAsync(ExcluirTemporadaRequest request)
    {
        var temporada = await _temporadaRepository.ObterPorIdAsync(request.Id);
        if (temporada == null)
            throw new Exception("Temporada não encontrada.");

        await _temporadaRepository.ExcluirAsync(request.Id);
    }
}