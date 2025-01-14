using Microsoft.Extensions.Caching.Distributed;
using NetSpace.Community.Application.Common.Caching;
using NetSpace.Community.Domain;
using System.Text.Json;

namespace NetSpace.Community.Infrastructure.Common;

public abstract class DistributedCacheBase<TEntity, TId>(IDistributedCache cache) : IDistributedCacheStorage<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

        await cache.SetAsync(entity.Id.ToString(), JsonSerializer.SerializeToUtf8Bytes(entity), options, cancellationToken);
    }

    public async Task RemoveByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        await cache.RemoveAsync(id.ToString(), cancellationToken);
    }

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken cancellationToken = default)
    {
        string cached = await cache.GetStringAsync(id.ToString(), cancellationToken);

        if (!string.IsNullOrWhiteSpace(cached))
        {
            var entity = JsonSerializer.Deserialize<TEntity>(cached);

            return entity;
        }

        return null;
    }
}
