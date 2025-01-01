using NetSpace.User.Domain;

namespace NetSpace.User.Application.Common.Cache;

/// <summary>
/// 
/// </summary>
public interface IDistributedCacheStorage<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    public Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default);
    public Task RemoveByIdAsync(TId id, CancellationToken cancellationToken = default);
    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default);
}
