using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Repositories;

public class AvaliacaoRepository : IAvaliacaoRepository
{
    private readonly KDramaDbContext _context;

    public AvaliacaoRepository(KDramaDbContext context)
    {
        _context = context;
    }

    public async Task<Avaliacao?> ObterPorUsuarioETemporadaAsync(Guid usuarioId, Guid temporadaId)
    {
        return await _context.Avaliacoes
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.UsuarioId == usuarioId && a.TemporadaId == temporadaId);
    }

    public async Task<bool> ExisteAvaliacaoAsync(Guid usuarioId, Guid temporadaId)
    {
        return await _context.Avaliacoes
            .AsNoTracking()
            .AnyAsync(a => a.UsuarioId == usuarioId && a.TemporadaId == temporadaId);
    }

    public async Task AdicionarAsync(Avaliacao avaliacao)
    {
        await _context.Avaliacoes.AddAsync(avaliacao);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Avaliacao avaliacao)
    {
        _context.Avaliacoes.Update(avaliacao);
        await _context.SaveChangesAsync();
    }
}