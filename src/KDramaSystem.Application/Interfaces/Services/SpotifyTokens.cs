namespace KDramaSystem.Application.Interfaces.Services;

public class SpotifyTokens
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public int ExpiresIn { get; set; }
    public DateTime ExpirationTime { get; set; }
}