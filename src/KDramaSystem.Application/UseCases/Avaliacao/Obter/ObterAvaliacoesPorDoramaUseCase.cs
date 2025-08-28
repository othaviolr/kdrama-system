using KDramaSystem.Application.DTOs.Avaliacao;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Avaliacao.Obter;

public class ObterAvaliacoesPorDoramaUseCase
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;

    public ObterAvaliacoesPorDoramaUseCase(IAvaliacaoRepository avaliacaoRepository)
    {
        _avaliacaoRepository = avaliacaoRepository;
    }

    public async Task<List<AvaliacaoPublicaDto>> ExecutarAsync(Guid doramaId)
    {
        if (doramaId == Guid.Empty)
            throw new ArgumentException("Id do dorama inválido.", nameof(doramaId));

        var avaliacoes = await _avaliacaoRepository.ObterPorDoramaAsync(doramaId);

        return avaliacoes.Select(a => new AvaliacaoPublicaDto
        {
            Id = a.Id,
            TemporadaId = a.TemporadaId,
            Nota = a.Nota.Valor,
            Comentario = a.Comentario?.Texto,
            UsuarioId = a.UsuarioId,
            UsuarioNome = a.Usuario.Nome,
            UsuarioFoto = a.Usuario.FotoUrl,
            RecomendadoPorUsuarioId = a.RecomendadoPorUsuarioId,
            RecomendadoPorNomeLivre = a.RecomendadoPorNomeLivre,
            DataAvaliacao = a.DataAvaliacao
        }).ToList();
    }
}