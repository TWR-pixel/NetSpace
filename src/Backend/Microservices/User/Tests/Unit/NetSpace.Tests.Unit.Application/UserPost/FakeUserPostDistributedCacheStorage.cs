﻿using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using NetSpace.User.Application.UserPost;
using NetSpace.User.Domain.UserPost;
using NetSpace.User.Tests.Unit.Initializer.Caching;
using System.Text.Json;

namespace NetSpace.User.Tests.Unit.Application.UserPost;

public sealed class FakeUserPostDistributedCacheStorage(IMemoryCache cache) : InMemoryDistributedCacheStorageBase<UserPostEntity, int>(cache), IUserPostDistributedCacheStorage
{
    public async Task<IEnumerable<UserPostEntity>?> GetLatest(CancellationToken cancellationToken = default)
    {
        cache.TryGetValue("userPosts:latest", out string cached);

        if (!string.IsNullOrWhiteSpace(cached))
        {
            var userPosts = JsonSerializer.Deserialize<IEnumerable<UserPostEntity>>(cached);

            return userPosts;
        }

        return null;
    }

    public async Task SetLatest(IEnumerable<UserPostEntity> entities, CancellationToken cancellationToken = default)
    {
        cache.Set("userPosts:latest", JsonSerializer.SerializeToUtf8Bytes(entities));
    }
}
