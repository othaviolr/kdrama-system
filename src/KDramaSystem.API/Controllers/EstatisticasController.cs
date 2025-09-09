using KDramaSystem.Application.Services;
using KDramaSystem.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EstatisticasController : ControllerBase
{
    private readonly EstatisticasService _estatisticasService;

    public EstatisticasController(EstatisticasService estatisticasService)
    {
        _estatisticasService = estatisticasService;
    }

    [HttpGet("{usuarioId:guid}")]
    public async Task<ActionResult<EstatisticasUsuario>> ObterPorUsuario(Guid usuarioId)
    {
        if (usuarioId == Guid.Empty)
            return BadRequest("Id de usuário inválido.");

        var estatisticas = await _estatisticasService.ObterPorUsuarioAsync(usuarioId);

        if (estatisticas == null)
            return NotFound();

        return Ok(estatisticas);
    }
}