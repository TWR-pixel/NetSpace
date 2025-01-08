using Microsoft.Extensions.Caching.Distributed;
using NetSpace.Community.Application.CommunityPost.Caching;
using NetSpace.Community.Domain.CommunityPost;
using NetSpace.Community.Infrastructure.Common;
using System.Text.Json;

namespace NetSpace.Community.Infrastructure.CommunityPost;

public sealed class CommunityPostDistributedCache(IDistributedCache cache) : DistributedCacheBase<CommunityPostEntity, int>(cache), ICommunityPostDistributedCache
{
    public async Task<IEnumerable<CommunityPostEntity>?> GetLatest(CancellationToken cancellationToken)
    {
        string cached = await cache.GetStringAsync("communityPosts:latest", cancellationToken);

        if (!string.IsNullOrWhiteSpace(cached))
        {
            var entities = JsonSerializer.Deserialize<IEnumerable<CommunityPostEntity>>(cached);

            return entities;
        }

        return null;
    }

    public async Task SetLatest(IEnumerable<CommunityPostEntity> entities, CancellationToken cancellationToken)
    {
        var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

        await cache.SetAsync("communityPosts:latest", JsonSerializer.SerializeToUtf8Bytes(entities), options, cancellationToken);
    }
}
