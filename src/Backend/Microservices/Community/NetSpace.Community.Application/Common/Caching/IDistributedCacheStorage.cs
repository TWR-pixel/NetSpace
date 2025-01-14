using NetSpace.Community.Domain;

namespace NetSpace.Community.Application.Common.Caching;

public interface IDistributedCacheStorage<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task RemoveByIdAsync(TId id, CancellationToken cancellationToken = default);
    public Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
}
