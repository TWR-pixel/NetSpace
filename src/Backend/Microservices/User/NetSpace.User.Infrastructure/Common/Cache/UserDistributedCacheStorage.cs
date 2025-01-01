using Microsoft.Extensions.Caching.Distributed;
using NetSpace.User.Application.Common.Cache;
using NetSpace.User.Domain.User;
using System.Text.Json;

namespace NetSpace.User.Infrastructure.Common.Cache;

public sealed class UserDistributedCacheStorage(IDistributedCache cache) : IUserDistributedCacheStorage
{
    public async Task AddAsync(UserEntity entity, CancellationToken cancellationToken = default)
    {
        await cache.SetAsync(entity.Id, JsonSerializer.SerializeToUtf8Bytes(entity), cancellationToken);
    }

    public async Task RemoveByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        await cache.RemoveAsync(id, cancellationToken);
    }

    public async Task<UserEntity?> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var userEntity = JsonSerializer.Deserialize<UserEntity>(await cache.GetAsync(id, cancellationToken));

        return userEntity;
    }

    public async Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var userEntity = JsonSerializer.Deserialize<UserEntity>(await cache.GetAsync(email, cancellationToken));

        return userEntity;
    }
}
