namespace KDramaSystem.Application.DTOs.Playlist;

public class CriarPlaylistDto
{
    public Guid DoramaId { get; set; }
    public string SpotifyPlaylistId { get; set; } = null!;
    public string Nome { get; set; } = null!;
    public string Url { get; set; } = null!;
    public string ImagemUrl { get; set; } = null!;
    public string Dono { get; set; } = null!;
    public int TotalMusicas { get; set; }
}