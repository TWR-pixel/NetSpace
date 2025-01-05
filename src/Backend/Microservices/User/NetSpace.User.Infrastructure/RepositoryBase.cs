using Microsoft.EntityFrameworkCore;
using NetSpace.User.Domain;
using NetSpace.User.UseCases;

namespace NetSpace.User.Infrastructure;

public abstract class RepositoryBase<TEntity, TId>(NetSpaceDbContext dbContext) : IRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{

    protected NetSpaceDbContext DbContext { get; set; } = dbContext;

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        return entry.Entity;
    }

    public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);

        return entities;
    }

    public virtual async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        var result = await DbContext.Set<TEntity>().AnyAsync(cancellationToken);

        return result;
    }

    public virtual async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        var result = await DbContext.Set<TEntity>().CountAsync(cancellationToken);

        return result;
    }

    public virtual Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Remove(entity);

        return Task.CompletedTask;
    }

    public virtual Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().RemoveRange(entities);

        return Task.CompletedTask;
    }

    public virtual async Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        var result = await DbContext.Set<TEntity>().FindAsync([id], cancellationToken);

        return result;
    }

    public virtual async Task<IEnumerable<TEntity>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await DbContext.Set<TEntity>().ToListAsync(cancellationToken);

        return result;
    }

    public virtual async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    public virtual Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().UpdateRange(entities);

        return Task.CompletedTask;
    }

    public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.SaveChangesAsync(cancellationToken);
    }
}
