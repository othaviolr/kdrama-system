using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Repositories;

public class ProgressoTemporadaRepository : IProgressoTemporadaRepository
{
    private readonly KDramaDbContext _context;

    public ProgressoTemporadaRepository(KDramaDbContext context)
    {
        _context = context;
    }

    public async Task<ProgressoTemporada?> ObterPorUsuarioETemporadaAsync(Guid usuarioId, Guid temporadaId)
    {
        return await _context.ProgressoTemporadas
            .FirstOrDefaultAsync(p => p.UsuarioId == usuarioId && p.TemporadaId == temporadaId);
    }

    public async Task CriarAsync (ProgressoTemporada progressoTemporada)
    {
        await _context.ProgressoTemporadas.AddAsync(progressoTemporada);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(ProgressoTemporada progressoTemporada)
    {
        _context.ProgressoTemporadas.Update(progressoTemporada);
        await _context.SaveChangesAsync();
    }

    public async Task ExcluirAsync(Guid progressoId)
    {
        var progresso = await _context.ProgressoTemporadas.FindAsync(progressoId);
        if (progresso != null)
        {
            _context.ProgressoTemporadas.Remove(progresso);
            await _context.SaveChangesAsync();
        }
    }
}