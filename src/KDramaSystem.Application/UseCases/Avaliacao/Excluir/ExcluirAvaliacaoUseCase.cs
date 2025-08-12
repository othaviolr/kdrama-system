using System.Security.Claims;
using KDramaSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace KDramaSystem.Application.UseCases.Avaliacao.Excluir;

public class ExcluirAvaliacaoUseCase
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ExcluirAvaliacaoValidator _validator;

    public ExcluirAvaliacaoUseCase(IAvaliacaoRepository avaliacaoRepository, IHttpContextAccessor httpContextAccessor, ExcluirAvaliacaoValidator validator)
    {
        _avaliacaoRepository = avaliacaoRepository;
        _httpContextAccessor = httpContextAccessor;
        _validator = validator;
    }

    public async Task ExecuteAsync(ExcluirAvaliacaoRequest request)
    {
        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
            throw new Exception(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

        var usuarioId = ObterUsuarioId();

        var avaliacao = await _avaliacaoRepository.ObterPorUsuarioETemporadaAsync(usuarioId, request.TemporadaId);
        if (avaliacao is null)
            throw new Exception("Avaliação não encontrada.");

        await _avaliacaoRepository.RemoverAsync(avaliacao);
    }

    private Guid ObterUsuarioId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userIdClaim))
            throw new Exception("Usuário não autenticado.");

        return Guid.Parse(userIdClaim);
    }
}