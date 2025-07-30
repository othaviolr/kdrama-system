namespace KDramaSystem.Domain.Interfaces;

public interface IUnitOfWork
{
    Task SalvarAlteracoesAsync();
}