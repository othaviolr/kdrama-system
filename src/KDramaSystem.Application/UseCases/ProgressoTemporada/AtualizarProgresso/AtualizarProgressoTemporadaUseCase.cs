using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.ValueObjects;

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
        var temporada = await _temporadaRepository.ObterPorIdAsync(request.TemporadaId);

        if (temporada == null)
            throw new Exception("Temporada não encontrada.");

        if (progresso == null)
        {
            progresso = new KDramaSystem.Domain.Entities.ProgressoTemporada(Guid.NewGuid(), usuarioId, request.TemporadaId, 0,
                new StatusDorama(Domain.Enums.StatusDoramaEnum.PlanejoAssistir)
            );

            await _progressoTemporadaRepository.CriarAsync(progresso);
        }
        var totalEpisodios = await _temporadaRepository.ContarEpisodiosAsync(request.TemporadaId);
        progresso.AtualizarProgresso(request.EpisodiosAssistidos, totalEpisodios);

        await _progressoTemporadaRepository.AtualizarAsync(progresso);
    }
}