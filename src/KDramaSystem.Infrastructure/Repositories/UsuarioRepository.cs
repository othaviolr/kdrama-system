using KDramaSystem.Domain.Entities;
using KDramaSystem.Domain.Interfaces.Repositories;
using KDramaSystem.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace KDramaSystem.Infrastructure.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly KDramaDbContext _context;

    public UsuarioRepository(KDramaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task AdicionarAsync(Usuario usuario)
    {
        if (usuario == null) throw new ArgumentNullException(nameof(usuario));

        await _context.Usuarios.AddAsync(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> NomeUsuarioExisteAsync(string nomeUsuario)
    {
        if (string.IsNullOrWhiteSpace(nomeUsuario))
            throw new ArgumentException("Nome de usuário deve ser informado.", nameof(nomeUsuario));

        return await _context.Usuarios.AnyAsync(u => u.NomeUsuario == nomeUsuario);
    }

    public async Task<Usuario?> ObterPorIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(id));

        return await _context.Usuarios.FindAsync(id);
    }

    public async Task SalvarAsync(Usuario usuario)
    {
        if (usuario == null) throw new ArgumentNullException(nameof(usuario));

        _context.Usuarios.Update(usuario);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Id inválido.", nameof(id));

        var usuario = await _context.Usuarios.FindAsync(id);
        if (usuario != null)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Usuario?> ObterPorNomeUsuarioAsync(string nomeUsuario)
    {
        return await _context.Usuarios
            .FirstOrDefaultAsync(u => u.NomeUsuario.ToLower() == nomeUsuario.ToLower());
    }
}