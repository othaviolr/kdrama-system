using KDramaSystem.Application.UseCases.Usuario.Registrar;
using KDramaSystem.Application.UseCases.Usuario.Login;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers
{
    [ApiController]
    [Route("api/usuarios")]
    public class UsuarioController : ControllerBase
    {
        private readonly RegistrarUsuarioHandler _registrarUsuarioHandler;
        private readonly LoginUsuarioHandler _loginUsuarioHandler;

        public UsuarioController(
            RegistrarUsuarioHandler registrarUsuarioHandler,
            LoginUsuarioHandler loginUsuarioHandler)
        {
            _registrarUsuarioHandler = registrarUsuarioHandler;
            _loginUsuarioHandler = loginUsuarioHandler;
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
                return Ok(result); // 200 OK
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
    }
}