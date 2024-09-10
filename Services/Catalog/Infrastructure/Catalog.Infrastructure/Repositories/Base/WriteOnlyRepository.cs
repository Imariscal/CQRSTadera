using Microsoft.EntityFrameworkCore;
using Catalog.Domain.Entities.Base.Contracts;
using Catalog.Domain.Repositories.Base;

namespace Catalog.Infrastructure.Repositories.Base;

public class WriteOnlyRepository<TKey, TEntity> :
    IWriteOnlyRepository<TKey, TEntity> where TEntity : class, IBaseEntity<TKey>, IAuditable<TKey>
{
    protected readonly IWriteOnlyUnitOfWork _uow;
    protected readonly DbSet<TEntity> _dbSet;

    public WriteOnlyRepository(IWriteOnlyUnitOfWork uow)
    {
        _uow = uow;
        _dbSet = _uow.Context.Set<TEntity>();
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        entity.Active = true;
        entity.CreationDate = DateTime.UtcNow;
        entity.CreatedBy = "Super visor";

        entity.LastModificationDate = DateTime.UtcNow;
        entity.LastModifiedBy = "Super visor";

        await _dbSet.AddAsync(entity);
        await _uow.SaveAsync();
    }

    public virtual async Task AddRangeAsync(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
        {
            entity.Active = true;
            entity.CreationDate = DateTime.UtcNow;
            entity.CreatedBy = "Super visor";

            entity.LastModificationDate = DateTime.UtcNow;
            entity.LastModifiedBy = "Super visor";

            await _dbSet.AddAsync(entity);
        }

        await _uow.SaveAsync();
    }

    public virtual async Task Modify(TEntity entity)
    {
        entity.LastModificationDate = DateTime.UtcNow;
        entity.LastModifiedBy = "Super visor";
        entity.Active = true;

        _dbSet.Update(entity);
        await _uow.SaveAsync();
    }

    public virtual async Task<bool> Remove(TEntity entity, bool applyPhysical = false)
    {
        if (applyPhysical) _dbSet.Remove(entity);
        else
        {
            entity.DeletionDate  = DateTime.UtcNow;
            entity.DeletedBy = "Super visor";
            entity.Active = false;

            _dbSet.Update(entity);
        }

        await _uow.SaveAsync();
        return true;
    }

    public virtual async Task<bool> Remove(TKey entityId, bool applyPhysical = false)
    {
        var entity = _dbSet.Find(entityId)??throw new ArgumentNullException(nameof(entityId));
        await Remove(entity, applyPhysical);

        return true;
    }

    public virtual async Task<bool> RemoveRange(IEnumerable<TEntity> entities, bool applyPhysical = false)
    {
        if (applyPhysical) _dbSet.RemoveRange(entities);
        else
        {
            foreach (var entity in entities)
            {
                entity.Active = false;
                entity.DeletionDate = DateTime.UtcNow;
                entity.DeletedBy = "Super visor";

                _dbSet.Remove(entity);
            }
        }

        await _uow.SaveAsync();
        return true;
    }

    public async Task<int> SaveAsync() => await _uow.SaveAsync();
}