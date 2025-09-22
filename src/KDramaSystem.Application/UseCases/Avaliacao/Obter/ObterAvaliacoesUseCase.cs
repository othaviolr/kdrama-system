using System.Security.Claims;
using KDramaSystem.Application.DTOs.Avaliacao;
using KDramaSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace KDramaSystem.Application.UseCases.Avaliacao.Obter;

public class ObterAvaliacoesUseCase
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ObterAvaliacoesUseCase(
        IAvaliacaoRepository avaliacaoRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _avaliacaoRepository = avaliacaoRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<IEnumerable<ObterAvaliacaoDto>> ExecutarAsync()
    {
        var usuarioId = ObterUsuarioId();

        var avaliacoes = await _avaliacaoRepository.ObterPorUsuarioAsync(usuarioId);

        return avaliacoes.Select(av => new ObterAvaliacaoDto
        {
            Id = av.Id,
            UsuarioId = av.UsuarioId,
            UsuarioNome = av.Usuario?.Nome,
            TemporadaId = av.TemporadaId,
            TemporadaNome = av.Temporada?.Nome,
            DoramaId = av.Temporada?.DoramaId ?? Guid.Empty,
            DoramaTitulo = av.Temporada?.Dorama?.Titulo,
            Nota = av.Nota.Valor,
            Comentario = av.Comentario?.Texto,
            RecomendadoPorUsuarioId = av.RecomendadoPorUsuarioId,
            RecomendadoPorNomeLivre = av.RecomendadoPorNomeLivre,
            DataAvaliacao = av.DataAvaliacao
        });
    }

    private Guid ObterUsuarioId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?
            .FindFirst(ClaimTypes.NameIdentifier)?.Value;

        if (string.IsNullOrWhiteSpace(userIdClaim))
            throw new Exception("Usuário não autenticado.");

        return Guid.Parse(userIdClaim);
    }
}