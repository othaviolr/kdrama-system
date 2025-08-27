using KDramaSystem.Application.Interfaces.Repositories;
using KDramaSystem.Domain.Entities;
using KDramaSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Repositories;

public class UsuarioRelacionamentoRepository : IUsuarioRelacionamentoRepository
{
    private readonly KDramaDbContext _context;

    public UsuarioRelacionamentoRepository(KDramaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> ExisteRelacionamentoAsync(Guid seguidorId, Guid seguindoId)
    {
        if (seguidorId == Guid.Empty)
            throw new ArgumentException("Id do seguidor inválido.", nameof(seguidorId));

        if (seguindoId == Guid.Empty)
            throw new ArgumentException("Id do seguindo inválido.", nameof(seguindoId));

        return await _context.UsuarioRelacionamentos
            .AnyAsync(r => r.SeguidorId == seguidorId && r.SeguindoId == seguindoId);
    }

    public async Task CriarAsync(UsuarioRelacionamento relacionamento)
    {
        if (relacionamento == null)
            throw new ArgumentNullException(nameof(relacionamento));

        await _context.UsuarioRelacionamentos.AddAsync(relacionamento);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Guid seguidorId, Guid seguindoId)
    {
        if (seguidorId == Guid.Empty)
            throw new ArgumentException("Id do seguidor inválido.", nameof(seguidorId));

        if (seguindoId == Guid.Empty)
            throw new ArgumentException("Id do seguindo inválido.", nameof(seguindoId));

        var existente = await _context.UsuarioRelacionamentos
            .FirstOrDefaultAsync(r => r.SeguidorId == seguidorId && r.SeguindoId == seguindoId);

        if (existente != null)
        {
            _context.UsuarioRelacionamentos.Remove(existente);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Usuario>> ObterSeguidoresAsync(Guid usuarioId)
    {
        return await _context.UsuarioRelacionamentos
            .Where(r => r.SeguindoId == usuarioId) 
            .Select(r => r.Seguidor) 
            .ToListAsync();
    }

    public async Task<List<Usuario>> ObterSeguindoAsync(Guid usuarioId)
    {
        return await _context.UsuarioRelacionamentos
            .Where(r => r.SeguidorId == usuarioId) 
            .Select(r => r.Seguindo) 
            .ToListAsync();
    }
}