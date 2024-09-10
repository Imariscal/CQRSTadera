
using Catalog.Domain.Entities.Base.Contracts;
using System.Linq.Expressions;

namespace Catalog.Domain.Repositories.Base;

public interface IReadOnlyRepository<TKey, TEntity> where TEntity : class, IBaseEntity<TKey>
{
    Task<TEntity?> GetAsync(TKey entityId);
    Task<TEntity?> GetAsync(TKey entityId, params string[]? includes);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<IEnumerable<TEntity>> GetAllAsync(params string[]? includes);
    Task<IEnumerable<TEntity>> GetAllMatchingAsync(
        Expression<Func<TEntity, bool>> filter,
        params string[] includes);
}