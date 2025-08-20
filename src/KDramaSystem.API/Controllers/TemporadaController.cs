using KDramaSystem.Application.UseCases.Temporada.Criar;
using KDramaSystem.Application.UseCases.Temporada.Editar;
using KDramaSystem.Application.UseCases.Temporada.Excluir;
using KDramaSystem.Application.UseCases.Temporada.Obter;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TemporadaController : ControllerBase
{
    private readonly CriarTemporadaUseCase _criarUseCase;
    private readonly EditarTemporadaUseCase _editarUseCase;
    private readonly ExcluirTemporadaUseCase _excluirUseCase;
    private readonly ObterTemporadaPorIdUseCase _obterUseCase;

    public TemporadaController(
        CriarTemporadaUseCase criarUseCase,
        EditarTemporadaUseCase editarUseCase,
        ExcluirTemporadaUseCase excluirUseCase,
        ObterTemporadaPorIdUseCase obterUseCase)
    {
        _criarUseCase = criarUseCase;
        _editarUseCase = editarUseCase;
        _excluirUseCase = excluirUseCase;
        _obterUseCase = obterUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarTemporadaRequest request)
    {
        var id = await _criarUseCase.ExecutarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(Guid id, [FromBody] EditarTemporadaRequest request)
    {
        request.Id = id;
        await _editarUseCase.ExecutarAsync(request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var request = new ExcluirTemporadaRequest { Id = id };
        await _excluirUseCase.ExecutarAsync(request);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var request = new ObterTemporadaPorIdRequest { Id = id };
        var temporadaDto = await _obterUseCase.ExecutarAsync(request);

        if (temporadaDto == null)
            return NotFound();

        return Ok(temporadaDto);
    }
}