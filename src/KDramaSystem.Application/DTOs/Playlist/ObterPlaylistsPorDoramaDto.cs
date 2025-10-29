namespace KDramaSystem.Application.DTOs.Playlist
{
    public class ObterPlaylistsPorDoramaDto
    {
        public string Id { get; set; }
        public string SpotifyPlaylistId { get; set; }
        public string Nome { get; set; }
        public string Url { get; set; }
        public string ImagemUrl { get; set; }
        public string Dono { get; set; }
        public int TotalMusicas { get; set; }

        public ObterPlaylistsPorDoramaDto(string id, string spotifyPlaylistId, string nome,string url, string imagemUrl, string dono, int totalMusicas)
        {
            Id = id;
            SpotifyPlaylistId = spotifyPlaylistId;
            Nome = nome;
            Url = url;
            ImagemUrl = imagemUrl;
            Dono = dono;
            TotalMusicas = totalMusicas;
        }

        public static ObterPlaylistsPorDoramaDto FromEntity(Domain.Entities.Playlist playlist)
        {
            return new ObterPlaylistsPorDoramaDto(playlist.Id.ToString(), playlist.SpotifyPlaylistId, playlist.Nome, playlist.Url, playlist.ImagemUrl, playlist.Dono, playlist.TotalMusicas);
        }
    }
}