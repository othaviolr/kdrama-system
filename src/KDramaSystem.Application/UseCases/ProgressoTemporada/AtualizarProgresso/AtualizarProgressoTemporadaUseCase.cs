using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.ProgressoTemporada.AtualizarProgresso;

public class AtualizarProgressoTemporadaUseCase
{
    private readonly IProgressoTemporadaRepository _progressoTemporadaRepository;
    private readonly ITemporadaRepository _temporadaRepository;

    public AtualizarProgressoTemporadaUseCase(
        IProgressoTemporadaRepository progressoTemporadaRepository,
        ITemporadaRepository temporadaRepository)
    {
        _progressoTemporadaRepository = progressoTemporadaRepository;
        _temporadaRepository = temporadaRepository;
    }

    public async Task ExecuteAsync(Guid usuarioId, AtualizarProgressoTemporadaRequest request)
    {
        var validator = new AtualizarProgressoTemporadaValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
            throw new Exception(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

        var progresso = await _progressoTemporadaRepository.ObterPorUsuarioETemporadaAsync(usuarioId, request.TemporadaId);

        if (progresso == null)
            throw new Exception("Progresso da temporada não encontrado.");

        var temporada = await _temporadaRepository.ObterPorIdAsync(request.TemporadaId);
        if (temporada == null)
            throw new Exception("Temporada não encontrada.");

        progresso.AtualizarProgresso(request.EpisodiosAssistidos, temporada.NumeroEpisodios);

        await _progressoTemporadaRepository.AtualizarAsync(progresso);
    }
}