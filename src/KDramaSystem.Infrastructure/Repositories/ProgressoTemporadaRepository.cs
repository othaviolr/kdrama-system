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
    public async Task<IEnumerable<ProgressoTemporada>> ObterPorUsuarioAsync(Guid usuarioId)
    {
        return await _context.ProgressoTemporadas
            .Include(p => p.Temporada)
                .ThenInclude(t => t.Dorama)
            .Include(p => p.Temporada)
                .ThenInclude(t => t.Episodios)
            .Where(p => p.UsuarioId == usuarioId)
            .ToListAsync();
    }

    public async Task<IEnumerable<ProgressoTemporada>> ObterTodosAsync()
    {
        return await _context.ProgressoTemporadas
            .Include(p => p.Temporada)
                .ThenInclude(t => t.Dorama)
            .ToListAsync();
    }

    public async Task<int> ContarDoramasConcluidosAsync(Guid usuarioId)
    {
        var progressoUsuario = await ObterPorUsuarioAsync(usuarioId);

        var progressoPorDorama = progressoUsuario
            .GroupBy(p => p.Temporada.DoramaId);

        int doramasConcluidos = 0;

        foreach (var grupo in progressoPorDorama)
        {
            bool doramaFinalizado = grupo.All(p => p.Status != null && p.Status.Valor == Domain.Enums.StatusDoramaEnum.Concluido);

            if (doramaFinalizado) doramasConcluidos++;
        }
        return doramasConcluidos;
    }

    public async Task<int> SomarTempoAssistidoAsync(Guid usuarioId)
    {
        var progressoUsuario = await ObterPorUsuarioAsync(usuarioId);

        int minutosTotais = 0;

        foreach (var progresso in progressoUsuario)
        {
            if (progresso.Temporada?.Episodios == null || progresso.Temporada.Episodios.Count == 0)
                continue;

            var episodiosAssistidos = progresso.Temporada.Episodios
                .OrderBy(e => e.Numero).Take(progresso.EpisodiosAssistidos);

            minutosTotais += episodiosAssistidos.Sum(e => e.DuracaoMinutos);
        }
        return minutosTotais;
    }
}