using KDramaSystem.Application.DTOs.Playlist;
using KDramaSystem.Application.UseCases.Playlist;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers
{
    [ApiController]
    [Route("api/playlists")]
    public class PlaylistController : ControllerBase
    {
        private readonly ObterPlaylistsPorDoramaUseCase _obterPlaylistsUseCase;
        private readonly AtribuirPlaylistAoDoramaUseCase _atribuirPlaylistUseCase;

        public PlaylistController(
            ObterPlaylistsPorDoramaUseCase obterPlaylistsUseCase,
            AtribuirPlaylistAoDoramaUseCase atribuirPlaylistUseCase)
        {
            _obterPlaylistsUseCase = obterPlaylistsUseCase;
            _atribuirPlaylistUseCase = atribuirPlaylistUseCase;
        }

        [HttpGet("doramas/{doramaId}")]
        public async Task<ActionResult<IEnumerable<ObterPlaylistsPorDoramaDto>>> GetPlaylists(Guid doramaId)
        {
            var playlists = await _obterPlaylistsUseCase.Execute(doramaId);
            return Ok(playlists);
        }

        [HttpPost]
        public async Task<IActionResult> AtribuirPlaylist([FromBody] CriarPlaylistDto dto)
        {
            await _atribuirPlaylistUseCase.Execute(dto);
            return Ok(new { message = "Playlist atribuída ao dorama com sucesso!" });
        }
    }
}