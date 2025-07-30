using KDramaSystem.Application.UseCases.Dorama;
using KDramaSystem.Application.UseCases.Dorama.Criar;
using KDramaSystem.Application.UseCases.Dorama.Editar;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers
{
    [ApiController]
    [Route("api/doramas")]
    public class DoramaController : ControllerBase
    {
        private readonly CriarDoramaUseCase _criarDoramaUseCase;
        private readonly EditarDoramaUseCase _editarDoramaUseCase;

        public DoramaController(
            CriarDoramaUseCase criarDoramaUseCase,
            EditarDoramaUseCase editarDoramaUseCase)
        {
            _criarDoramaUseCase = criarDoramaUseCase;
            _editarDoramaUseCase = editarDoramaUseCase;
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
                await _editarDoramaUseCase.ExecutarAsync(id, request);
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
            catch (Exception ex)
            {
                return StatusCode(500, new { erro = ex.Message, stackTrace = ex.StackTrace });
            }
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            return Ok();
        }
    }
}