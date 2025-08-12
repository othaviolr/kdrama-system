using KDramaSystem.Application.DTOs.Avaliacao;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.Interfaces.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace KDramaSystem.Application.UseCases.Avaliacao.Obter;

public class ObterAvaliacaoUseCase
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ObterAvaliacaoUseCase(
        IAvaliacaoRepository avaliacaoRepository,
        IHttpContextAccessor httpContextAccessor)
    {
        _avaliacaoRepository = avaliacaoRepository;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ObterAvaliacaoDto?> ExecutarAsync(Guid temporadaId)
    {
        var usuarioId = ObterUsuarioId();

        var avaliacao = await _avaliacaoRepository.ObterPorUsuarioETemporadaAsync(usuarioId, temporadaId);
        if (avaliacao == null) return null;

        return new ObterAvaliacaoDto
        {
            Id = avaliacao.Id,
            TemporadaId = avaliacao.TemporadaId,
            Nota = avaliacao.Nota.Valor,
            Comentario = avaliacao.Comentario?.Texto,
            RecomendadoPorUsuarioId = avaliacao.RecomendadoPorUsuarioId,
            RecomendadoPorNomeLivre = avaliacao.RecomendadoPorNomeLivre,
            DataAvaliacao = avaliacao.DataAvaliacao
        };
    }

    private Guid ObterUsuarioId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userIdClaim))
            throw new Exception("Usuário não autenticado.");

        return Guid.Parse(userIdClaim);
    }
}