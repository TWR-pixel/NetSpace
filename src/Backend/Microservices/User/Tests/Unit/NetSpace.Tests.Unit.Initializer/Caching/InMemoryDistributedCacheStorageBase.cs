using Microsoft.Extensions.Caching.Memory;
using NetSpace.User.Application.Common.Cache;
using NetSpace.User.Domain;

namespace NetSpace.Tests.Unit.Initializer.Caching;

public class InMemoryDistributedCacheStorageBase<TEntity, TId>(IMemoryCache cache) : IDistributedCacheStorage<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    public Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        cache.Set(entity, cancellationToken);

        return Task.CompletedTask;
    }

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        cache.TryGetValue(id, out var entity);

        return (TEntity?)entity;
    }

    public Task RemoveByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        cache.Remove(id);

        return Task.CompletedTask;
    }
}
