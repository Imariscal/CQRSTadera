using Catalog.Persistence.Contexts.Base;

namespace Catalog.Infrastructure.Repositories.Base;

public interface IWriteOnlyUnitOfWork : IReadOnlyUnitOfWork
{
    Task<int> SaveAsync();
    Task BeginTransactionAsync();
    Task RollBackTransactionAsync();
    Task CommitTransactionAsync();
}