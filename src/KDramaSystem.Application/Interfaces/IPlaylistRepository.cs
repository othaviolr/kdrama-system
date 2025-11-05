using KDramaSystem.Domain.Entities;

namespace KDramaSystem.Application.Interfaces;

public interface IPlaylistRepository
{
    Task AdicionarAsync(Playlist playlist);
    Task<List<Playlist>> ObterPorDoramaIdAsync(Guid doramaId);
    Task<Playlist?> ObterPorSpotifyIdEPorDoramaIdAsync(string spotifyPlaylistId, Guid doramaId);
    Task RemoverAsync(Playlist playlist);
    // adicionar AtualizarAsync futuramente
}