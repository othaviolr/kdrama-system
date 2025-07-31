using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Temporada.Criar;

public class CriarTemporadaUseCase
{
    private readonly IDoramaRepository _doramaRepository;
    private readonly ITemporadaRepository _temporadaRepository;

    public CriarTemporadaUseCase(
        IDoramaRepository doramaRepository,
        ITemporadaRepository temporadaRepository)
    {
        _doramaRepository = doramaRepository;
        _temporadaRepository = temporadaRepository;
    }

    public async Task<Guid> ExecutarAsync(CriarTemporadaRequest request)
    {
        var dorama = await _doramaRepository.ObterPorIdAsync(request.DoramaId);
        if (dorama == null)
            throw new Exception("Dorama não encontrado.");

        var temporadasExistentes = await _temporadaRepository
            .ObterPorDoramaIdAsync(request.DoramaId);

        if (temporadasExistentes.Any(t => t.Numero == request.Numero))
            throw new InvalidOperationException($"Já existe uma temporada número {request.Numero} para este dorama.");

        var temporada = new Domain.Entities.Temporada(
            Guid.NewGuid(),
            request.DoramaId,
            request.Numero,
            request.AnoLancamento,
            request.EmExibicao,
            request.Nome,
            request.Sinopse
        );

        await _temporadaRepository.AdicionarAsync(temporada);

        return temporada.Id;
    }
}