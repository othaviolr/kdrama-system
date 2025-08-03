using KDramaSystem.Domain.Interfaces;
using KDramaSystem.Infrastructure.Persistence;
using System;
using System.Threading.Tasks;

namespace KDramaSystem.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork, IDisposable
{
    private readonly KDramaDbContext _context;

    public UnitOfWork(KDramaDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task SalvarAlteracoesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}