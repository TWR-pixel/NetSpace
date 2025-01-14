using Microsoft.Extensions.Caching.Memory;
using NetSpace.Community.Application.Common.Caching;
using NetSpace.Community.Domain;

namespace NetSpace.Community.Tests.Unit.Application;

public class FakeInMemoryDistributedCache<TEntity, TId>(IMemoryCache cache) : IDistributedCacheStorage<TEntity, TId>
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
