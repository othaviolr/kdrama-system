using KDramaSystem.Application.DTOs.Playlist;
using KDramaSystem.Application.Interfaces;

namespace KDramaSystem.Application.UseCases.Playlist;

public class ObterPlaylistsPorDoramaUseCase
{
    private readonly IPlaylistRepository _playlistRepository;

    public ObterPlaylistsPorDoramaUseCase(IPlaylistRepository playlistRepository)
    {
        _playlistRepository = playlistRepository;
    }

    public async Task<IEnumerable<ObterPlaylistDto>> Execute(Guid doramaId)
    {
        var playlists = await _playlistRepository.ObterPorDoramaIdAsync(doramaId);

        return playlists.Select(p => new ObterPlaylistDto
        {
            Id = p.Id.ToString(),   
            SpotifyPlaylistId = p.SpotifyPlaylistId,
            Nome = p.Nome,
            Url = p.Url,
            ImagemUrl = p.ImagemUrl,
            Dono = p.Dono,
            TotalMusicas = p.TotalMusicas
        });
    }
}