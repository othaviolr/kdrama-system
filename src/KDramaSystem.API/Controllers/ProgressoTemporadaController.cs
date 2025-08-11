using KDramaSystem.Application.DTOs.ProgressoTemporada;
using KDramaSystem.Application.Interfaces;
using KDramaSystem.Application.UseCases.ProgressoTemporada.AtualizarProgresso;
using KDramaSystem.Application.UseCases.ProgressoTemporada.AtualizarStatus;
using KDramaSystem.Application.UseCases.ProgressoTemporada.ExcluirProgresso;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers
{
    [ApiController]
    [Route("api/progresso-temporada")]
    [Authorize]
    public class ProgressoTemporadaController : ControllerBase
    {
        private readonly AtualizarProgressoTemporadaUseCase _atualizarProgressoUseCase;
        private readonly AtualizarStatusTemporadaUseCase _atualizarStatusUseCase;
        private readonly ExcluirProgressoTemporadaUseCase _excluirProgressoUseCase;
        private readonly IUsuarioAutenticadoProvider _usuarioAutenticadoProvider;

        public ProgressoTemporadaController(
            AtualizarProgressoTemporadaUseCase atualizarProgressoUseCase,
            AtualizarStatusTemporadaUseCase atualizarStatusUseCase,
            ExcluirProgressoTemporadaUseCase excluirProgressoUseCase,
            IUsuarioAutenticadoProvider usuarioAutenticadoProvider)
        {
            _atualizarProgressoUseCase = atualizarProgressoUseCase;
            _atualizarStatusUseCase = atualizarStatusUseCase;
            _excluirProgressoUseCase = excluirProgressoUseCase;
            _usuarioAutenticadoProvider = usuarioAutenticadoProvider;
        }

        [HttpPut("progresso")]
        public async Task<IActionResult> AtualizarProgresso([FromBody] AtualizarProgressoTemporadaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();

                var request = new AtualizarProgressoTemporadaRequest
                {
                    TemporadaId = dto.TemporadaId,
                    EpisodiosAssistidos = dto.EpisodiosAssistidos
                };

                await _atualizarProgressoUseCase.ExecuteAsync(usuarioId, request);

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

        [HttpPut("status")]
        public async Task<IActionResult> AtualizarStatus([FromBody] AtualizarStatusTemporadaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();

                var request = new AtualizarStatusTemporadaRequest
                {
                    TemporadaId = dto.TemporadaId,
                    Status = dto.Status
                };

                await _atualizarStatusUseCase.ExecuteAsync(usuarioId, request);

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
        public async Task<IActionResult> ExcluirProgresso(Guid temporadaId)
        {
            if (temporadaId == Guid.Empty)
                return BadRequest(new { erro = "TemporadaId inválido." });

            try
            {
                var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();

                var request = new ExcluirProgressoTemporadaRequest
                {
                    TemporadaId = temporadaId
                };

                await _excluirProgressoUseCase.ExecuteAsync(usuarioId, request);

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
    }
}