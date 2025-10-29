using KDramaSystem.Application.DTOs.Playlist;
using KDramaSystem.Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers
{
    [ApiController]
    [Route("api/spotify")]
    public class SpotifyController : ControllerBase
    {
        private readonly SpotifyService _spotifyService;

        public SpotifyController(SpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        [HttpGet("playlists")]
        public async Task<ActionResult<IEnumerable<ObterPlaylistsPorDoramaDto>>> BuscarPlaylists([FromQuery] string nomeDorama)
        {
            var playlists = await _spotifyService.BuscarPlaylistsPorNomeAsync(nomeDorama);
            return Ok(playlists);
        }
    }
}