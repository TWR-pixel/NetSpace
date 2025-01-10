using Microsoft.Extensions.Caching.Distributed;
using NetSpace.User.Application.UserPost;
using NetSpace.User.Domain.UserPost;
using NetSpace.User.Infrastructure.Common.Cache;
using System.Text.Json;

namespace NetSpace.User.Infrastructure.UserPost;

public sealed class UserPostDistributedCacheStorage(IDistributedCache cache) : DistributedCacheStorageBase<UserPostEntity, int>(cache), IUserPostDistributedCacheStorage
{
    public async Task<IEnumerable<UserPostEntity>?> GetLatest(CancellationToken cancellationToken = default)
    {
        string cached = await Cache.GetStringAsync("userPosts:latest", cancellationToken);

        if (!string.IsNullOrWhiteSpace(cached))
        {
            var userPosts = JsonSerializer.Deserialize<IEnumerable<UserPostEntity>>(cached);

            return userPosts;
        }

        return null;
    }

    public async Task SetLatest(IEnumerable<UserPostEntity> entities, CancellationToken cancellationToken = default)
    {
        var options = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromSeconds(30));

        await Cache.SetAsync("userPosts:latest", JsonSerializer.SerializeToUtf8Bytes(entities), options, cancellationToken);
    }
}
