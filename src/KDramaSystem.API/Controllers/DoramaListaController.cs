using KDramaSystem.Application.DTOs.DoramaLista;
using KDramaSystem.Application.UseCases.DoramaLista.Adicionar;
using KDramaSystem.Application.UseCases.DoramaLista.Remover;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers;

[ApiController]
[Route("api/dorama-lista")]
[Authorize]
public class DoramaListaController : ControllerBase
{
    private readonly AdicionarDoramaListaUseCase _adicionarUseCase;
    private readonly RemoverDoramaListaUseCase _removerUseCase;

    public DoramaListaController(
        AdicionarDoramaListaUseCase adicionarUseCase,
        RemoverDoramaListaUseCase removerUseCase)
    {
        _adicionarUseCase = adicionarUseCase;
        _removerUseCase = removerUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] AdicionarDoramaListaDto dto)
    {
        await _adicionarUseCase.ExecuteAsync(dto.ListaPrateleiraId, dto.DoramaId);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Remover([FromBody] RemoverDoramaListaDto dto)
    {
        await _removerUseCase.ExecuteAsync(dto.ListaPrateleiraId, dto.DoramaId);
        return NoContent();
    }
}