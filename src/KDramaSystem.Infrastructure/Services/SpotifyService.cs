using KDramaSystem.Application.DTOs.Playlist;
using System.Net.Http.Headers;
using System.Text.Json;

namespace KDramaSystem.Infrastructure.Services
{
    public class SpotifyService
    {
        private readonly HttpClient _httpClient;
        private readonly string _token;

        public SpotifyService(HttpClient httpClient, string token)
        {
            _httpClient = httpClient;
            _token = token;
        }

        public async Task<IEnumerable<ObterPlaylistsPorDoramaDto>> BuscarPlaylistsPorNomeAsync(string nomeDorama)
        {
            var url = $"https://api.spotify.com/v1/search?q={Uri.EscapeDataString(nomeDorama)}&type=playlist&limit=5";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

            using var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
                throw new Exception("Erro ao buscar playlists no Spotify.");

            var json = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(json);

            var playlists = new List<ObterPlaylistsPorDoramaDto>();

            foreach (var item in doc.RootElement.GetProperty("playlists").GetProperty("items").EnumerateArray())
            {
                playlists.Add(new ObterPlaylistsPorDoramaDto(
                    item.GetProperty("id").GetString(),
                    item.GetProperty("id").GetString(),
                    item.GetProperty("name").GetString(),
                    item.GetProperty("external_urls").GetProperty("spotify").GetString(),
                    item.GetProperty("images")[0].GetProperty("url").GetString(),
                    item.GetProperty("owner").GetProperty("display_name").GetString(),
                    item.GetProperty("tracks").GetProperty("total").GetInt32()
                ));
            }

            return playlists;
        }
    }
}