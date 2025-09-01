using KDramaSystem.Application.DTOs.Dorama;
using KDramaSystem.Application.UseCases.Dorama;
using KDramaSystem.Application.UseCases.Dorama.Criar;
using KDramaSystem.Application.UseCases.Dorama.Editar;
using KDramaSystem.Application.UseCases.Dorama.Excluir;
using KDramaSystem.Application.UseCases.Dorama.Obter;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers
{
    [ApiController]
    [Route("api/doramas")]
    public class DoramaController : ControllerBase
    {
        private readonly CriarDoramaUseCase _criarDoramaUseCase;
        private readonly EditarDoramaUseCase _editarDoramaUseCase;
        private readonly ExcluirDoramaUseCase _excluirDoramaUseCase;
        private readonly ObterDoramaUseCase _obterDoramaUseCase;
        private readonly ObterDoramaCompletoUseCase _obterDoramaCompletoUseCase;
        private readonly ObterTodosDoramasCompletosUseCase _obterTodosDoramasCompletosUseCase;

        public DoramaController(
            CriarDoramaUseCase criarDoramaUseCase,
            EditarDoramaUseCase editarDoramaUseCase,
            ExcluirDoramaUseCase excluirDoramaUseCase,
            ObterDoramaUseCase obterDoramaUseCase,
            ObterDoramaCompletoUseCase obterDoramaCompletoUseCase,
            ObterTodosDoramasCompletosUseCase obterTodosDoramasCompletosUseCase)
        {
            _criarDoramaUseCase = criarDoramaUseCase;
            _editarDoramaUseCase = editarDoramaUseCase;
            _excluirDoramaUseCase = excluirDoramaUseCase;
            _obterDoramaUseCase = obterDoramaUseCase;
            _obterDoramaCompletoUseCase = obterDoramaCompletoUseCase;
            _obterTodosDoramasCompletosUseCase = obterTodosDoramasCompletosUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CriarDorama([FromBody] CriarDoramaRequest request)
        {
            try
            {
                var resultado = await _criarDoramaUseCase.ExecutarAsync(request);
                return CreatedAtAction(nameof(ObterPorId), new { id = resultado }, new { Id = resultado });
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

        [HttpPut("{id}")]
        public async Task<IActionResult> EditarDorama(Guid id, [FromBody] EditarDoramaRequest request)
        {
            try
            {
                request.DoramaId = id;
                await _editarDoramaUseCase.ExecutarAsync(request);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(Guid id)
        {
            try
            {
                var dorama = await _obterDoramaUseCase.ExecutarAsync(new ObterDoramaRequest { Id = id });
                if (dorama == null)
                    return NotFound(new { erro = "Dorama não encontrado." });

                return Ok(dorama);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> ExcluirDorama(Guid id, [FromQuery] Guid usuarioId)
        {
            try
            {
                var dto = new ExcluirDoramaDto
                {
                    DoramaId = id,
                    UsuarioId = usuarioId
                };

                var request = new ExcluirDoramaRequest
                {
                    Id = dto.DoramaId,
                    UsuarioId = dto.UsuarioId
                };

                await _excluirDoramaUseCase.ExecutarAsync(request);

                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpGet("{id}/completo")]
        public async Task<IActionResult> ObterDoramaCompleto(Guid id)
        {
            var dorama = await _obterDoramaCompletoUseCase.ExecutarAsync(id);
            if (dorama == null)
                return NotFound();

            return Ok(dorama);
        }

        [HttpGet("completo")]
        public async Task<IActionResult> ObterTodosDoramasCompletos()
        {
            var doramas = await _obterTodosDoramasCompletosUseCase.ExecutarAsync();
            return Ok(doramas);
        }

        [HttpGet("titulo/{titulo}")]
        public async Task<IActionResult> ObterPorTitulo(string titulo)
        {
            var dorama = await _obterDoramaUseCase.ExecutarPorTituloAsync(titulo);
            if (dorama == null) return NotFound();
            return Ok(dorama);
        }
    }
}