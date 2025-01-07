﻿using Microsoft.Extensions.Caching.Distributed;
using NetSpace.User.Application.User;
using NetSpace.User.Domain.User;
using NetSpace.User.Infrastructure.Common.Cache;
using System.Text.Json;

namespace NetSpace.User.Infrastructure.User;

public sealed class UserDistributedCacheStorage(IDistributedCache cache) : DistributedCacheStorageBase<UserEntity, Guid>(cache), IUserDistributedCacheStorage
{
    public async Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        var userEntity = JsonSerializer.Deserialize<UserEntity>(await Cache.GetAsync(email, cancellationToken));

        return userEntity;
    }
}
