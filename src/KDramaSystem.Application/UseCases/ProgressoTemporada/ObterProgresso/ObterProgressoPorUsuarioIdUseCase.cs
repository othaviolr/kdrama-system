using KDramaSystem.Application.DTOs.ProgressoTemporada;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.ProgressoTemporada.ObterProgresso;

public class ObterProgressoPorUsuarioIdUseCase
{
    private readonly IProgressoTemporadaRepository _repository;

    public ObterProgressoPorUsuarioIdUseCase(IProgressoTemporadaRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<ObterProgressoTemporadaDto>> ExecuteAsync(Guid usuarioId)
    {
        var progressos = await _repository.ObterPorUsuarioAsync(usuarioId);

        return progressos.Select(p => new ObterProgressoTemporadaDto
        {
            Id = p.Id,
            UsuarioId = p.UsuarioId,
            TemporadaId = p.TemporadaId,
            EpisodiosAssistidos = p.EpisodiosAssistidos,
            Status = p.Status.Valor,
            DataAtualizacao = p.DataAtualizacao
        });
    }
}