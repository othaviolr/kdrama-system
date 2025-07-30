using KDramaSystem.Domain.Interfaces;

namespace KDramaSystem.Infrastructure.Repositories;

public class UnitOfWork : IUnitOfWork
{
    public Task SalvarAlteracoesAsync()
    {
        return Task.CompletedTask;
    }
}