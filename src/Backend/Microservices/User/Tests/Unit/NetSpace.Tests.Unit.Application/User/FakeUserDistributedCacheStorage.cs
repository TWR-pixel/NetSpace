using Microsoft.Extensions.Caching.Memory;
using NetSpace.Tests.Unit.Initializer.Caching;
using NetSpace.User.Application.User;
using NetSpace.User.Domain.User;

namespace NetSpace.Tests.Unit.Application.User;

public sealed class FakeUserDistributedCacheStorage(IMemoryCache cache) : InMemoryDistributedCacheStorageBase<UserEntity, Guid>(cache), IUserDistributedCacheStorage
{
}
