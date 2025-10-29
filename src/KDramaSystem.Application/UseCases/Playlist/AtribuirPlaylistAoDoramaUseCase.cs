using KDramaSystem.Application.DTOs.Playlist;
using KDramaSystem.Application.Interfaces;
using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Application.UseCases.Playlist;

public class AtribuirPlaylistAoDoramaUseCase
{
    private readonly IPlaylistRepository _playlistRepository;
    private readonly IDoramaRepository _doramaRepository;

    public AtribuirPlaylistAoDoramaUseCase(IPlaylistRepository playlistRepository, IDoramaRepository doramaRepository)
    {
        _playlistRepository = playlistRepository;
        _doramaRepository = doramaRepository;
    }

    public async Task Execute(CriarPlaylistDto dto)
    {
        var dorama = await _doramaRepository.ObterPorIdAsync(dto.DoramaId);
        if (dorama == null)
            throw new Exception("Dorama não encontrado.");

        // evita duplicidade da mesma playlist para o mesmo dorama
        var existente = await _playlistRepository.ObterPorSpotifyIdEPorDoramaIdAsync(dto.SpotifyPlaylistId, dto.DoramaId);
        if (existente != null)
            throw new Exception("Playlist já vinculada a esse dorama.");

        var playlist = new Domain.Entities.Playlist(dto.SpotifyPlaylistId, dto.Nome, dto.Url, dto.ImagemUrl, dto.Dono, dto.TotalMusicas, dto.DoramaId
        );
        await _playlistRepository.AdicionarAsync(playlist);
    }
}