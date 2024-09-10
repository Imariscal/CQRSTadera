using Microsoft.EntityFrameworkCore;

namespace Catalog.Persistence.Contexts.Base;

public interface IReadOnlyUnitOfWork : IDisposable
{
    DbContext Context { get; }
}
