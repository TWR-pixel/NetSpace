using Microsoft.EntityFrameworkCore;
using NetSpace.User.Domain;
using NetSpace.User.Infrastructure.Common;
using NetSpace.User.UseCases;

namespace NetSpace.User.Infrastructure;

public abstract class RepositoryBase<TEntity, TId>(NetSpaceDbContext dbContext) : IRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{

    protected NetSpaceDbContext DbContext { get; set; } = dbContext;

    public async Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var entry = await DbContext.Set<TEntity>().AddAsync(entity, cancellationToken);

        return entry.Entity;
    }

    public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        await DbContext.Set<TEntity>().AddRangeAsync(entities, cancellationToken);

        return entities;
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        var result = await DbContext.Set<TEntity>().AnyAsync(cancellationToken);

        return result;
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        var result = await DbContext.Set<TEntity>().CountAsync(cancellationToken);

        return result;
    }

    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Remove(entity);

        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().RemoveRange(entities);

        return Task.CompletedTask;
    }

    public async Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        var result = await DbContext.Set<TEntity>().FindAsync([id], cancellationToken);

        return result;
    }

    public async Task<IEnumerable<TEntity>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var result = await DbContext.Set<TEntity>().ToListAsync(cancellationToken);

        return result;
    }

    public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().Update(entity);
    }

    public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default)
    {
        DbContext.Set<TEntity>().UpdateRange(entities);

        return Task.CompletedTask;
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.SaveChangesAsync(cancellationToken);
    }
}
