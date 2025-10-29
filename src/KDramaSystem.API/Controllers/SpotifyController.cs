using KDramaSystem.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace KDramaSystem.API.Controllers;

[ApiController]
[Route("api/spotify")]
public class SpotifyController : ControllerBase
{
    private readonly ISpotifyService _spotifyService;

    public SpotifyController(ISpotifyService spotifyService)
    {
        _spotifyService = spotifyService;
    }

    /// <summary>
    /// Busca playlists do Spotify com base no nome do dorama.
    /// </summary>
    [HttpGet("playlists")]
    public async Task<ActionResult<IEnumerable<SpotifyPlaylistSearchResultDto>>> BuscarPlaylists([FromQuery] string nomeDorama)
    {
        if (string.IsNullOrWhiteSpace(nomeDorama))
            return BadRequest("O nome do dorama deve ser fornecido.");

        try
        {
            var playlists = await _spotifyService.BuscarPlaylistsAsync(nomeDorama);
            return Ok(playlists);
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro ao buscar playlists: {ex.Message}");
        }
    }
}