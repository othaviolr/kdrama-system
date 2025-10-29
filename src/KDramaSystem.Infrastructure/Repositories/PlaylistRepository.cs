using KDramaSystem.Application.Interfaces;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Repositories;

public class PlaylistRepository : IPlaylistRepository
{
    private readonly KDramaDbContext _context;

    public PlaylistRepository(KDramaDbContext context)
    {
        _context = context;
    }

    public async Task AdicionarAsync(Playlist playlist)
    {
        _context.Playlists.Add(playlist);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Playlist>> ObterPorDoramaIdAsync(Guid doramaId)
    {
        return await _context.Playlists
            .Where(p => p.DoramaId == doramaId)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Playlist?> ObterPorSpotifyIdEPorDoramaIdAsync(string spotifyPlaylistId, Guid doramaId)
    {
        return await _context.Playlists
            .FirstOrDefaultAsync(p => p.SpotifyPlaylistId == spotifyPlaylistId && p.DoramaId == doramaId);
    }

    public async Task RemoverAsync(Playlist playlist)
    {
        _context.Playlists.Remove(playlist);
        await _context.SaveChangesAsync();
    }
}