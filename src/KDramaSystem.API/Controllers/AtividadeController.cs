using KDramaSystem.Application.Interfaces;
using KDramaSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/atividades")]
public class AtividadeController : ControllerBase
{
    private readonly IAtividadeService _atividadeService;
    private readonly IUsuarioAutenticadoProvider _usuarioAutenticadoProvider;

    public AtividadeController(IAtividadeService atividadeService, IUsuarioAutenticadoProvider usuarioAutenticadoProvider)
    {
        _atividadeService = atividadeService;
        _usuarioAutenticadoProvider = usuarioAutenticadoProvider;
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

    [HttpGet("minhas")]
    public async Task<IActionResult> ObterMinhas()
    {
        try
        {
            var usuarioId = _usuarioAutenticadoProvider.ObterUsuarioId();
            var atividades = await _atividadeService.ObterAtividadesUsuarioAsync(usuarioId);
            return Ok(atividades);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { erro = ex.Message });
        }
    }
}