using KDramaSystem.Application.UseCases.Usuario.Editar;
using KDramaSystem.Application.UseCases.Usuario.ObterPerfilCompleto;
using KDramaSystem.Application.UseCases.Usuario.Registrar;
using KDramaSystem.Application.UseCases.Usuario.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly RegistrarUsuarioHandler _registrarUsuarioHandler;
        private readonly LoginUsuarioHandler _loginUsuarioHandler;
        private readonly IObterPerfilCompletoUseCase _obterPerfilCompletoUseCase;
        private readonly IEditarPerfilUseCase _editarPerfilUseCase;

        public UsuarioController(
            RegistrarUsuarioHandler registrarUsuarioHandler,
            LoginUsuarioHandler loginUsuarioHandler,
            IObterPerfilCompletoUseCase obterPerfilCompletoUseCase,
            IEditarPerfilUseCase editarPerfilUseCase)
        {
            _registrarUsuarioHandler = registrarUsuarioHandler;
            _loginUsuarioHandler = loginUsuarioHandler;
            _obterPerfilCompletoUseCase = obterPerfilCompletoUseCase;
            _editarPerfilUseCase = editarPerfilUseCase;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioCommand command)
        {
            try
            {
                var result = await _registrarUsuarioHandler.Handle(command);
                return CreatedAtAction(nameof(Registrar), result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUsuarioCommand command)
        {
            try
            {
                var result = await _loginUsuarioHandler.Handle(command);
                return Ok(result);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [Authorize]
        [HttpGet("perfil")]
        public async Task<IActionResult> ObterPerfilCompleto()
        {
            var perfil = await _obterPerfilCompletoUseCase.ExecutarAsync(new());
            if (perfil == null)
                return NotFound();

            return Ok(perfil);
        }

        [Authorize]
        [HttpPut("perfil")]
        public async Task<IActionResult> EditarPerfil([FromBody] EditarPerfilRequest request)
        {
            try
            {
                await _editarPerfilUseCase.ExecutarAsync(request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }
    }
}