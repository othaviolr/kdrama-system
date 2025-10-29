namespace KDramaSystem.Domain.Entities;

public class Playlist
{
    public Guid Id { get; private set; }
    public string SpotifyPlaylistId { get; private set; }
    public string Nome { get; private set; }
    public string Url { get; private set; }
    public string ImagemUrl { get; private set; }
    public string Dono { get; private set; }
    public int TotalMusicas { get; private set; }

    public Guid DoramaId { get; private set; }
    public Dorama Dorama { get; private set; }

    private Playlist() { }

    public Playlist(string spotifyPlaylistId, string nome, string url, string imagemUrl, string dono, int totalMusicas, Guid doramaId)
    {
        Id = Guid.NewGuid();
        SpotifyPlaylistId = spotifyPlaylistId ?? throw new ArgumentNullException(nameof(spotifyPlaylistId));
        Nome = nome ?? throw new ArgumentNullException(nameof(nome));
        Url = url ?? throw new ArgumentNullException(nameof(url));
        ImagemUrl = imagemUrl;
        Dono = dono;
        TotalMusicas = totalMusicas;
        DoramaId = doramaId;
    }

    public void AtualizarMetadados(string nome, string url, string imagemUrl, string dono, int totalMusicas)
    {
        Nome = nome ?? Nome;
        Url = url ?? Url;
        ImagemUrl = imagemUrl ?? ImagemUrl;
        Dono = dono ?? Dono;
        TotalMusicas = totalMusicas;
    }
}