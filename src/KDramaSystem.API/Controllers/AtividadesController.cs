using KDramaSystem.Application.DTOs.Atividade;
using KDramaSystem.Application.UseCases.Atividade.Obter;
using KDramaSystem.Application.UseCases.Atividade.Registrar;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers;

[ApiController]
[Route("api/atividades")]
public class AtividadesController : ControllerBase
{
    private readonly RegistrarAtividadeUseCase _registrarAtividadeUseCase;
    private readonly ObterAtividadesUsuarioUseCase _obterAtividadesUsuarioUseCase;

    public AtividadesController(
        RegistrarAtividadeUseCase registrarAtividadeUseCase,
        ObterAtividadesUsuarioUseCase obterAtividadesUsuarioUseCase)
    {
        _registrarAtividadeUseCase = registrarAtividadeUseCase;
        _obterAtividadesUsuarioUseCase = obterAtividadesUsuarioUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> RegistrarAtividade([FromBody] RegistrarAtividadeDto dto)
    {
        var request = new RegistrarAtividadeRequest
        {
            UsuarioId = dto.UsuarioId,
            Tipo = dto.Tipo,
            ReferenciaId = dto.ReferenciaId
        };

        await _registrarAtividadeUseCase.ExecuteAsync(request);

        return CreatedAtAction(nameof(ObterAtividadesUsuario), new { usuarioId = dto.UsuarioId }, null);
    }

    [HttpGet("usuario/{usuarioId:guid}")]
    public async Task<IActionResult> ObterAtividadesUsuario(Guid usuarioId)
    {
        var request = new ObterAtividadesUsuarioRequest { UsuarioId = usuarioId };
        IEnumerable<ObterAtividadeDto> atividades = await _obterAtividadesUsuarioUseCase.ExecuteAsync(request);
        return Ok(atividades);
    }
}