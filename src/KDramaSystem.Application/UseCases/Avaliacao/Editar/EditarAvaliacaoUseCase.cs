using System.Security.Claims;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Domain.ValueObjects;
using KDramaSystem.Domain.ValueObjetcs;
using Microsoft.AspNetCore.Http;

namespace KDramaSystem.Application.UseCases.Avaliacao.Editar;

public class EditarAvaliacaoUseCase
{
    private readonly IAvaliacaoRepository _avaliacaoRepository;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly EditarAvaliacaoValidator _validator;

    public EditarAvaliacaoUseCase(IAvaliacaoRepository avaliacaoRepository, IHttpContextAccessor httpContextAccessor, EditarAvaliacaoValidator validator)
    {
        _avaliacaoRepository = avaliacaoRepository;
        _httpContextAccessor = httpContextAccessor;
        _validator = validator;
    }

    public async Task ExecuteAsync(EditarAvaliacaoRequest request)
    {
        var validationResult = _validator.Validate(request);
        if (!validationResult.IsValid)
            throw new Exception(string.Join(" | ", validationResult.Errors.Select(e => e.ErrorMessage)));

        var usuarioId = ObterUsuarioId();

        var avaliacao = await _avaliacaoRepository.ObterPorUsuarioETemporadaAsync(usuarioId, request.TemporadaId);
        if (avaliacao is null)
            throw new Exception("Avaliação não encontrada.");

        avaliacao.AtualizarNota(new Nota(request.Nota));

        if (!string.IsNullOrWhiteSpace(request.Comentario))
            avaliacao.AtualizarComentario(new ComentarioValor(request.Comentario));
        else
            avaliacao.AtualizarComentario(null);

        avaliacao.AtualizarRecomendacao(request.RecomendadoPorUsuarioId, request.RecomendadoPorNomeLivre);

        await _avaliacaoRepository.AtualizarAsync(avaliacao);
    }

    private Guid ObterUsuarioId()
    {
        var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrWhiteSpace(userIdClaim))
            throw new Exception("Usuário não autenticado.");

        return Guid.Parse(userIdClaim);
    }
}