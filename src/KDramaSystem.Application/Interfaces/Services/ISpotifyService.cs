namespace KDramaSystem.Application.Interfaces.Services;

public class SpotifyPlaylistSearchResultDto
{
    public string SpotifyPlaylistId { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string ImagemUrl { get; set; } = null!;
    public string Dono { get; set; } = null!;
    public int TotalMusicas { get; set; }
}

public interface ISpotifyService
{
    Task<IEnumerable<SpotifyPlaylistSearchResultDto>> BuscarPlaylistsAsync(string query);
}