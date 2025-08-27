using KDramaSystem.Application.UseCases.Usuario.Editar;
using KDramaSystem.Application.UseCases.Usuario.ObterPerfilCompleto;
using KDramaSystem.Application.UseCases.Usuario.Registrar;
using KDramaSystem.Application.UseCases.Usuario.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using KDramaSystem.Application.UseCases.Usuario.Deletar;
using KDramaSystem.Application.UseCases.Usuario;
using KDramaSystem.Application.UseCases.Usuario.DeixarDeSeguir;
using KDramaSystem.Application.UseCases.Usuario.Seguir;
using KDramaSystem.Application.UseCases.Usuario.ObterPerfilPublico;
using System.Security.Claims;
using KDramaSystem.Application.DTOs.Usuario;

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
        private readonly IDeletarPerfilUseCase _deletarPerfilUseCase;
        private readonly SeguirUsuarioUseCase _seguirUsuarioUseCase;
        private readonly DeixarDeSeguirUsuarioUseCase _deixarDeSeguirUsuarioUseCase;
        private readonly IObterPerfilPublicoUseCase _obterPerfilPublicoUseCase;
        private readonly ObterSeguidoresUseCase _obterSeguidoresUseCase;
        private readonly ObterSeguindoUseCase _obterSeguindoUseCase;

        public UsuarioController(
            RegistrarUsuarioHandler registrarUsuarioHandler,
            LoginUsuarioHandler loginUsuarioHandler,
            IObterPerfilCompletoUseCase obterPerfilCompletoUseCase,
            IEditarPerfilUseCase editarPerfilUseCase,
            IDeletarPerfilUseCase deletarPerfilUseCase,
            SeguirUsuarioUseCase seguirUsuarioUseCase,
            DeixarDeSeguirUsuarioUseCase deixarDeSeguirUsuarioUseCase,
            IObterPerfilPublicoUseCase obterPerfilPublicoUseCase,
            ObterSeguidoresUseCase obterSeguidoresUseCase,
            ObterSeguindoUseCase obterSeguindoUseCase)
        {
            _registrarUsuarioHandler = registrarUsuarioHandler;
            _loginUsuarioHandler = loginUsuarioHandler;
            _obterPerfilCompletoUseCase = obterPerfilCompletoUseCase;
            _editarPerfilUseCase = editarPerfilUseCase;
            _deletarPerfilUseCase = deletarPerfilUseCase;
            _seguirUsuarioUseCase = seguirUsuarioUseCase;
            _deixarDeSeguirUsuarioUseCase = deixarDeSeguirUsuarioUseCase;
            _obterPerfilPublicoUseCase = obterPerfilPublicoUseCase;
            _obterSeguidoresUseCase = obterSeguidoresUseCase;
            _obterSeguindoUseCase = obterSeguindoUseCase;
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

        [HttpGet("{nomeUsuario}")]
        public async Task<IActionResult> ObterPerfilPublico(string nomeUsuario)
        {
            Guid? usuarioLogadoId = null;

            if (User.Identity?.IsAuthenticated == true)
            {
                var claimSub = User.FindFirst("sub")?.Value;

                if (!string.IsNullOrWhiteSpace(claimSub) && Guid.TryParse(claimSub, out var guid))
                {
                    usuarioLogadoId = guid;
                }
            }

            var perfil = await _obterPerfilPublicoUseCase.ExecutarAsync(nomeUsuario, usuarioLogadoId);
            if (perfil == null) return NotFound();

            return Ok(perfil);
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

        [Authorize]
        [HttpDelete("perfil")]
        public async Task<IActionResult> DeletarPerfil()
        {
            try
            {
                await _deletarPerfilUseCase.ExecutarAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("{id}/seguir")]
        public async Task<IActionResult> SeguirUsuario(Guid id)
        {
            try
            {
                var usuarioLogadoIdClaim =
                    User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                    User.FindFirst("sub")?.Value;

                if (usuarioLogadoIdClaim is null)
                    throw new Exception("Usuário não autenticado");

                var usuarioLogadoId = Guid.Parse(usuarioLogadoIdClaim);

                var request = new SeguirUsuarioRequest(id);
                await _seguirUsuarioUseCase.ExecutarAsync(usuarioLogadoId, request);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { erro = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [Authorize]
        [HttpPost("{id}/deixar-de-seguir")]
        public async Task<IActionResult> DeixarDeSeguirUsuario(Guid id)
        {
            try
            {
                var usuarioLogadoIdClaim =
                    User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                    User.FindFirst("sub")?.Value;

                if (usuarioLogadoIdClaim is null)
                    throw new Exception("Usuário não autenticado");

                var usuarioLogadoId = Guid.Parse(usuarioLogadoIdClaim);

                var request = new DeixarDeSeguirUsuarioRequest(id);
                await _deixarDeSeguirUsuarioUseCase.ExecutarAsync(usuarioLogadoId, request);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        [HttpGet("{usuarioId}/seguidores")]
        public async Task<IActionResult> ObterSeguidores(Guid usuarioId)
        {
            var result = await _obterSeguidoresUseCase.ExecutarAsync(usuarioId);
            return Ok(result);
        }

        [HttpGet("{usuarioId}/seguindo")]
        public async Task<IActionResult> ObterSeguindo(Guid usuarioId)
        {
            var result = await _obterSeguindoUseCase.ExecutarAsync(usuarioId);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("seguidores")]
        public async Task<IActionResult> ObterMeusSeguidores()
        {
            var usuarioLogadoId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                User.FindFirst("sub")?.Value ?? throw new Exception("Usuário não autenticado")
            );

            var result = await _obterSeguidoresUseCase.ExecutarAsync(usuarioLogadoId);
            return Ok(result);
        }

        [Authorize]
        [HttpGet("seguindo")]
        public async Task<IActionResult> ObterMeusSeguindo()
        {
            var usuarioLogadoId = Guid.Parse(
                User.FindFirstValue(ClaimTypes.NameIdentifier) ??
                User.FindFirst("sub")?.Value ?? throw new Exception("Usuário não autenticado")
            );

            var result = await _obterSeguindoUseCase.ExecutarAsync(usuarioLogadoId);
            return Ok(result);
        }
    }
}