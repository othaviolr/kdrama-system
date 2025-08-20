using KDramaSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/atividades")]
public class AtividadeController : ControllerBase
{
    private readonly IAtividadeService _atividadeService;

    public AtividadeController(IAtividadeService atividadeService)
    {
        _atividadeService = atividadeService;
    }

    [HttpGet("feed/{quantidade}")]
    public async Task<IActionResult> ObterFeed(int quantidade)
    {
        var atividades = await _atividadeService.ObterFeedAsync(quantidade);
        return Ok(atividades);
    }

    [HttpGet("usuario/{usuarioId}")]
    public async Task<IActionResult> ObterAtividadesUsuario(Guid usuarioId)
    {
        var atividades = await _atividadeService.ObterAtividadesUsuarioAsync(usuarioId);
        return Ok(atividades);
    }
}