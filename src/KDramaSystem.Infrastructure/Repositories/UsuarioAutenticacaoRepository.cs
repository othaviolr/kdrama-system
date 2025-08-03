using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces.Repositories;
using KDramaSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System;

namespace KDramaSystem.Infrastructure.Repositories;

public class UsuarioAutenticacaoRepository : IUsuarioAutenticacaoRepository
{
    private readonly KDramaDbContext _context;

    public UsuarioAutenticacaoRepository(KDramaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<bool> EmailExisteAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email deve ser informado.", nameof(email));

        return await _context.Set<UsuarioAutenticacao>()
                             .AnyAsync(u => u.Email == email);
    }

    public async Task<UsuarioAutenticacao?> ObterPorEmailAsync(string email)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email deve ser informado.", nameof(email));

        return await _context.Set<UsuarioAutenticacao>()
                             .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task SalvarAsync(UsuarioAutenticacao usuarioAutenticacao)
    {
        if (usuarioAutenticacao == null)
            throw new ArgumentNullException(nameof(usuarioAutenticacao));

        await _context.Set<UsuarioAutenticacao>().AddAsync(usuarioAutenticacao);
        await _context.SaveChangesAsync();
    }
}