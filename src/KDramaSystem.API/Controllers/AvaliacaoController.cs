using KDramaSystem.Application.DTOs.Avaliacao;
using KDramaSystem.Application.Interfaces;
using KDramaSystem.Application.UseCases.Avaliacao.Criar;
using KDramaSystem.Application.UseCases.Avaliacao.Editar;
using KDramaSystem.Application.UseCases.Avaliacao.Excluir;
using KDramaSystem.Application.UseCases.Avaliacao.Obter;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers;

[ApiController]
[Route("api/avaliacoes")]
[Authorize]
public class AvaliacaoController : ControllerBase
{
    private readonly CriarAvaliacaoUseCase _criarAvaliacaoUseCase;
    private readonly EditarAvaliacaoUseCase _editarAvaliacaoUseCase;
    private readonly ExcluirAvaliacaoUseCase _excluirAvaliacaoUseCase;
    private readonly ObterAvaliacaoUseCase _obterAvaliacaoUseCase;
    private readonly IUsuarioAutenticadoProvider _usuarioAutenticadoProvider;

    public AvaliacaoController(CriarAvaliacaoUseCase criarAvaliacaoUseCase,
        EditarAvaliacaoUseCase editarAvaliacaoUseCase,
        ExcluirAvaliacaoUseCase excluirAvaliacaoUseCase,
        ObterAvaliacaoUseCase obterAvaliacaoUseCase,
        IUsuarioAutenticadoProvider usuarioAutenticadoProvider)
    {
        _criarAvaliacaoUseCase = criarAvaliacaoUseCase;
        _editarAvaliacaoUseCase = editarAvaliacaoUseCase;
        _excluirAvaliacaoUseCase = excluirAvaliacaoUseCase;
        _obterAvaliacaoUseCase = obterAvaliacaoUseCase;
        _usuarioAutenticadoProvider = usuarioAutenticadoProvider;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarAvaliacaoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();

            var request = new CriarAvaliacaoRequest
            {
                TemporadaId = dto.TemporadaId,
                Nota = dto.Nota,
                Comentario = dto.Comentario,
                RecomendadoPorUsuarioId = dto.RecomendadoPorUsuarioId,
                RecomendadoPorNomeLivre = dto.RecomendadoPorNomeLivre
            };

            await _criarAvaliacaoUseCase.ExecuteAsync(request);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = ex.Message });
        }
    }

    [HttpPut]
    public async Task<IActionResult> Editar([FromBody] EditarAvaliacaoDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();

            var request = new EditarAvaliacaoRequest
            {
                TemporadaId = dto.TemporadaId,
                Nota = dto.Nota,
                Comentario = dto.Comentario,
                RecomendadoPorUsuarioId = dto.RecomendadoPorUsuarioId,
                RecomendadoPorNomeLivre = dto.RecomendadoPorNomeLivre
            };

            await _editarAvaliacaoUseCase.ExecuteAsync(request);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = ex.Message });
        }
    }

    [HttpDelete("{temporadaId:guid}")]
    public async Task<IActionResult> Excluir(Guid temporadaId)
    {
        if (temporadaId == Guid.Empty)
            return BadRequest(new { erro = "TemporadaId inválido." });

        try
        {
            var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();

            var request = new ExcluirAvaliacaoRequest
            {
                TemporadaId = temporadaId
            };

            await _excluirAvaliacaoUseCase.ExecuteAsync(request);

            return NoContent();
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = ex.Message });
        }
    }

    [HttpGet("{temporadaId:guid}")]
    public async Task<IActionResult> Obter(Guid temporadaId)
    {
        if (temporadaId == Guid.Empty)
            return BadRequest(new { erro = "TemporadaId inválido." });

        try
        {
            var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();

            var dto = await _obterAvaliacaoUseCase.ExecutarAsync(temporadaId);

            if (dto == null)
                return NotFound(new { erro = "Avaliação não encontrada." });

            return Ok(dto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { erro = ex.Message });
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { erro = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = ex.Message });
        }
    }
}