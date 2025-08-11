using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.ValueObjects;

namespace KDramaSystem.Application.UseCases.ProgressoTemporada.AtualizarStatus;

public class AtualizarStatusTemporadaUseCase
{
    private readonly IProgressoTemporadaRepository _progressoTemporadaRepository;

    public AtualizarStatusTemporadaUseCase(IProgressoTemporadaRepository progressoTemporadaRepository)
    {
        _progressoTemporadaRepository = progressoTemporadaRepository;
    }

    public async Task ExecuteAsync(Guid usuarioId, AtualizarStatusTemporadaRequest request)
    {
        var validator = new AtualizarStatusTemporadaValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
            throw new Exception(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

        var progresso = await _progressoTemporadaRepository.ObterPorUsuarioETemporadaAsync(usuarioId, request.TemporadaId);

        if (progresso == null)
            throw new Exception("Progresso da temporada não encontrado.");

        progresso.AtualizarStatus(new StatusDorama(request.Status));

        await _progressoTemporadaRepository.AtualizarAsync(progresso);
    }
}