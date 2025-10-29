namespace KDramaSystem.Application.DTOs.Playlist;

public class SpotifyTokensDto
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public int ExpiresIn { get; set; }
}