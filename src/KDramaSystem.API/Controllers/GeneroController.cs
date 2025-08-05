using KDramaSystem.Application.UseCases.Genero.Criar;
using KDramaSystem.Application.UseCases.Genero.Editar;
using KDramaSystem.Application.UseCases.Genero.Excluir;
using KDramaSystem.Application.UseCases.Genero.Obter;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GeneroController : ControllerBase
{
    private readonly CriarGeneroUseCase _criarUseCase;
    private readonly EditarGeneroUseCase _editarUseCase;
    private readonly ExcluirGeneroUseCase _excluirUseCase;
    private readonly ObterGeneroPorIdUseCase _obterPorIdUseCase;

    public GeneroController(
        CriarGeneroUseCase criarUseCase,
        EditarGeneroUseCase editarUseCase,
        ExcluirGeneroUseCase excluirUseCase,
        ObterGeneroPorIdUseCase obterPorIdUseCase)
    {
        _criarUseCase = criarUseCase;
        _editarUseCase = editarUseCase;
        _excluirUseCase = excluirUseCase;
        _obterPorIdUseCase = obterPorIdUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarGeneroRequest request)
    {
        var id = await _criarUseCase.ExecutarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(Guid id, [FromBody] EditarGeneroRequest request)
    {
        request.Id = id;
        await _editarUseCase.ExecutarAsync(request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var request = new ExcluirGeneroRequest { Id = id };
        await _excluirUseCase.ExecutarAsync(request);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var genero = await _obterPorIdUseCase.ExecutarAsync(id);
        if (genero == null)
            return NotFound();

        return Ok(genero);
    }
}