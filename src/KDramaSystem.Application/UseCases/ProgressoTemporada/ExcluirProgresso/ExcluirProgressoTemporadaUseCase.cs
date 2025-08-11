using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.ProgressoTemporada.ExcluirProgresso;

public class ExcluirProgressoTemporadaUseCase
{
    private readonly IProgressoTemporadaRepository _progressoTemporadaRepository;

    public ExcluirProgressoTemporadaUseCase(IProgressoTemporadaRepository progressoTemporadaRepository)
    {
        _progressoTemporadaRepository = progressoTemporadaRepository;
    }

    public async Task ExecuteAsync(Guid usuarioId, ExcluirProgressoTemporadaRequest request)
    {
        var validator = new ExcluirProgressoTemporadaValidator();
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
            throw new Exception(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

        var progresso = await _progressoTemporadaRepository.ObterPorUsuarioETemporadaAsync(usuarioId, request.TemporadaId);

        if (progresso == null)
            throw new Exception("Progresso da temporada não encontrado.");

        await _progressoTemporadaRepository.ExcluirAsync(progresso.Id);
    }
}