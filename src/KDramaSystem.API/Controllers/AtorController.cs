using KDramaSystem.Application.UseCases.Ator.Criar;
using KDramaSystem.Application.UseCases.Ator.Editar;
using KDramaSystem.Application.UseCases.Ator.Excluir;
using KDramaSystem.Application.UseCases.Ator.Obter;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AtorController : ControllerBase
{
    private readonly CriarAtorUseCase _criarUseCase;
    private readonly EditarAtorUseCase _editarUseCase;
    private readonly ExcluirAtorUseCase _excluirUseCase;
    private readonly ObterAtorUseCase _obterUseCase;

    public AtorController(
        CriarAtorUseCase criarUseCase,
        EditarAtorUseCase editarUseCase,
        ExcluirAtorUseCase excluirUseCase,
        ObterAtorUseCase obterUseCase)
    {
        _criarUseCase = criarUseCase;
        _editarUseCase = editarUseCase;
        _excluirUseCase = excluirUseCase;
        _obterUseCase = obterUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarAtorRequest request)
    {
        var id = await _criarUseCase.ExecutarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(Guid id, [FromBody] EditarAtorRequest request)
    {
        request.Id = id;
        await _editarUseCase.ExecutarAsync(request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var request = new ExcluirAtorRequest { Id = id };
        await _excluirUseCase.ExecutarAsync(request);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var request = new ObterAtorRequest { Id = id };
        var ator = await _obterUseCase.ExecutarAsync(request);
        return Ok(ator);
    }

    [HttpGet("nome/{nome}")]
    public async Task<IActionResult> ObterPorNome(string nome)
    {
        var ator = await _obterUseCase.ExecutarPorNomeAsync(nome);
        if (ator == null) return NotFound();
        return Ok(ator);
    }

    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var atores = await _obterUseCase.ExecutarTodosAsync();
        var atoresResumo = atores.Select(a => new
        {
            a.Id,
            a.Nome,
            a.FotoUrl
        }).ToList();

        return Ok(atoresResumo);
    }
}