using KDramaSystem.Application.DTOs.Avaliacao;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Avaliacao.Obter;

public class ObterAvaliacoesPorUsuarioUseCase
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;

    public ObterAvaliacoesPorUsuarioUseCase(IAvaliacaoRepository avaliacaoRepository)
    {
        _avaliacaoRepository = avaliacaoRepository;
    }

    public async Task<IEnumerable<ObterAvaliacaoDto>> ExecutarAsync(Guid usuarioId)
    {
        var avaliacoes = await _avaliacaoRepository.ObterPorUsuarioAsync(usuarioId);

        return avaliacoes.Select(a => new ObterAvaliacaoDto
        {
            Id = a.Id,
            UsuarioId = a.UsuarioId,            
            TemporadaId = a.TemporadaId,
            Nota = a.Nota.Valor,                 
            Comentario = a.Comentario?.Texto,   
            RecomendadoPorUsuarioId = a.RecomendadoPorUsuarioId,
            RecomendadoPorNomeLivre = a.RecomendadoPorNomeLivre,
            DataAvaliacao = a.DataAvaliacao
        });
    }
}