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
        {
            progresso = new KDramaSystem.Domain.Entities.ProgressoTemporada(Guid.NewGuid(), usuarioId, request.TemporadaId, 0,
                new StatusDorama(request.Status));

            await _progressoTemporadaRepository.CriarAsync(progresso);
        }
        else
        {
            progresso.AtualizarStatus(new StatusDorama(request.Status));
            await _progressoTemporadaRepository.AtualizarAsync(progresso);
        }
    }
}