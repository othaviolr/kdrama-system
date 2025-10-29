using KDramaSystem.Application.Interfaces.Services;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

public class SpotifyService : ISpotifyService
{
    private readonly HttpClient _httpClient;
    private string? _accessToken;
    private DateTime _expiration;

    private const string CLIENT_ID = "98493d06ed644ea2a0dac44a50bb25d1";
    private const string CLIENT_SECRET = "314c70d303334b6abd535a5eca2d608d";

    public SpotifyService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    private async Task<string> GetAccessTokenAsync()
    {
        if (_accessToken != null && _expiration > DateTime.UtcNow.AddMinutes(1))
            return _accessToken;

        var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{CLIENT_ID}:{CLIENT_SECRET}"));
        var request = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
        request.Headers.Authorization = new AuthenticationHeaderValue("Basic", authToken);
        request.Content = new FormUrlEncodedContent(new Dictionary<string, string>
        {
            { "grant_type", "client_credentials" }
        });

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(json);

        _accessToken = doc.RootElement.GetProperty("access_token").GetString();
        var expiresIn = doc.RootElement.GetProperty("expires_in").GetInt32();
        _expiration = DateTime.UtcNow.AddSeconds(expiresIn);

        return _accessToken!;
    }

    public async Task<IEnumerable<SpotifyPlaylistSearchResultDto>> BuscarPlaylistsAsync(string query)
    {
        var accessToken = await GetAccessTokenAsync();

        var url = $"https://api.spotify.com/v1/search?q={Uri.EscapeDataString(query)}&type=playlist&limit=5";
        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        var response = await _httpClient.SendAsync(request);
        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        var doc = JsonDocument.Parse(json);

        var playlists = new List<SpotifyPlaylistSearchResultDto>();
        foreach (var item in doc.RootElement.GetProperty("playlists").GetProperty("items").EnumerateArray())
        {
            playlists.Add(new SpotifyPlaylistSearchResultDto
            {
                SpotifyPlaylistId = item.GetProperty("id").GetString()!,
                Nome = item.GetProperty("name").GetString()!,
                Url = item.GetProperty("external_urls").GetProperty("spotify").GetString()!,
                ImagemUrl = item.GetProperty("images")[0].GetProperty("url").GetString()!,
                Dono = item.GetProperty("owner").GetProperty("display_name").GetString()!,
                TotalMusicas = item.GetProperty("tracks").GetProperty("total").GetInt32()
            });
        }

        return playlists;
    }
}