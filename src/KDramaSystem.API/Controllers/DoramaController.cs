using KDramaSystem.Application.UseCases.Dorama.Criar;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers
{
    [ApiController]
    [Route("api/doramas")]
    public class DoramaController : ControllerBase
    {
        private readonly CriarDoramaUseCase _criarDoramaUseCase;

        public DoramaController(CriarDoramaUseCase criarDoramaUseCase)
        {
            _criarDoramaUseCase = criarDoramaUseCase;
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

        [HttpGet("{id}")]
        public IActionResult ObterPorId(Guid id)
        {
            return Ok();
        }
    }
}