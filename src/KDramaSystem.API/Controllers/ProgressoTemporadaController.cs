using KDramaSystem.Application.DTOs.ProgressoTemporada;
using KDramaSystem.Application.UseCases.ProgressoTemporada.AtualizarProgresso;
using KDramaSystem.Application.UseCases.ProgressoTemporada.AtualizarStatus;
using KDramaSystem.Application.UseCases.ProgressoTemporada.ExcluirProgresso;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers
{
    [ApiController]
    [Route("api/progresso-temporada")]
    public class ProgressoTemporadaController : ControllerBase
    {
        private readonly AtualizarProgressoTemporadaUseCase _atualizarProgressoUseCase;
        private readonly AtualizarStatusTemporadaUseCase _atualizarStatusUseCase;
        private readonly ExcluirProgressoTemporadaUseCase _excluirProgressoUseCase;

        public ProgressoTemporadaController(
            AtualizarProgressoTemporadaUseCase atualizarProgressoUseCase,
            AtualizarStatusTemporadaUseCase atualizarStatusUseCase,
            ExcluirProgressoTemporadaUseCase excluirProgressoUseCase)
        {
            _atualizarProgressoUseCase = atualizarProgressoUseCase;
            _atualizarStatusUseCase = atualizarStatusUseCase;
            _excluirProgressoUseCase = excluirProgressoUseCase;
        }

        [HttpPut("progresso")]
        public async Task<IActionResult> AtualizarProgresso([FromBody] AtualizarProgressoTemporadaDto dto)
        {
            try
            {
                var usuarioId = ObterUsuarioIdDoToken();

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
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpPut("status")]
        public async Task<IActionResult> AtualizarStatus([FromBody] AtualizarStatusTemporadaDto dto)
        {
            try
            {
                var usuarioId = ObterUsuarioIdDoToken();

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
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> ExcluirProgresso([FromBody] ExcluirProgressoTemporadaDto dto)
        {
            try
            {
                var usuarioId = ObterUsuarioIdDoToken();

                var request = new ExcluirProgressoTemporadaRequest
                {
                    TemporadaId = dto.TemporadaId
                };

                await _excluirProgressoUseCase.ExecuteAsync(usuarioId, request);

                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        private Guid ObterUsuarioIdDoToken()
        {
            var claim = User.Claims.FirstOrDefault(c => c.Type == "sub");
            if (claim == null)
                throw new Exception("Usuário não autenticado.");

            return Guid.Parse(claim.Value);
        }
    }
}