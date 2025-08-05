using KDramaSystem.Application.UseCases.Episodio.Criar;
using KDramaSystem.Application.UseCases.Episodio.Editar;
using KDramaSystem.Application.UseCases.Episodio.Excluir;
using KDramaSystem.Application.UseCases.Episodio.Obter;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EpisodioController : ControllerBase
{
    private readonly CriarEpisodioUseCase _criarUseCase;
    private readonly EditarEpisodioUseCase _editarUseCase;
    private readonly ExcluirEpisodioUseCase _excluirUseCase;
    private readonly ObterEpisodioPorIdUseCase _obterPorIdUseCase;

    public EpisodioController(
        CriarEpisodioUseCase criarUseCase,
        EditarEpisodioUseCase editarUseCase,
        ExcluirEpisodioUseCase excluirUseCase,
        ObterEpisodioPorIdUseCase obterPorIdUseCase)
    {
        _criarUseCase = criarUseCase;
        _editarUseCase = editarUseCase;
        _excluirUseCase = excluirUseCase;
        _obterPorIdUseCase = obterPorIdUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarEpisodioRequest request)
    {
        var id = await _criarUseCase.ExecutarAsync(request);
        return CreatedAtAction(nameof(ObterPorId), new { id }, null);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(Guid id, [FromBody] EditarEpisodioRequest request)
    {
        request.Id = id;
        await _editarUseCase.ExecutarAsync(request);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(Guid id)
    {
        var request = new ExcluirEpisodioRequest { EpisodioId = id };
        await _excluirUseCase.ExecutarAsync(request);
        return NoContent();
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(Guid id)
    {
        var episodio = await _obterPorIdUseCase.ExecutarAsync(id);
        if (episodio == null)
            return NotFound();

        return Ok(episodio);
    }
}