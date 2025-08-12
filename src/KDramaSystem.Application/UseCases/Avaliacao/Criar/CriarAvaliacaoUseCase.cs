using System.Security.Claims;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.ValueObjects;
using KDramaSystem.Domain.ValueObjetcs;
using Microsoft.AspNetCore.Http;

namespace KDramaSystem.Application.UseCases.Avaliacao.Criar;

public class CriarAvaliacaoUseCase
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly CriarAvaliacaoValidator _validator;

    public CriarAvaliacaoUseCase(IAvaliacaoRepository avaliacaoRepository, IHttpContextAccessor httpContextAccessor, CriarAvaliacaoValidator validator)
    {
        _avaliacaoRepository = avaliacaoRepository;
        _httpContextAccessor = httpContextAccessor;
        _validator = validator;
    }

    public async Task ExecuteAsync(CriarAvaliacaoRequest request)
    {
        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
            throw new Exception(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

        var usuarioId = ObterUsuarioId();

        if (await _avaliacaoRepository.ExisteAvaliacaoAsync(usuarioId, request.TemporadaId))
            throw new Exception("Você já avaliou esta temporada.");

        var nota = new Nota(request.Nota);
        ComentarioValor? comentario = null;
        if (!string.IsNullOrWhiteSpace(request.Comentario))
            comentario = new ComentarioValor(request.Comentario);

        var avaliacao = new Domain.Entities.Avaliacao(Guid.NewGuid(), usuarioId, request.TemporadaId, nota, comentario, request.RecomendadoPorUsuarioId, request.RecomendadoPorNomeLivre);

        await _avaliacaoRepository.AdicionarAsync(avaliacao);
    }

    private Guid ObterUsuarioId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userIdClaim))
            throw new Exception("Usuário não autenticado.");

        return Guid.Parse(userIdClaim);
    }
}