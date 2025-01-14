using Microsoft.Extensions.Caching.Memory;
using NetSpace.User.Application.User;
using NetSpace.User.Domain.User;
using NetSpace.User.Tests.Unit.Initializer.Caching;

namespace NetSpace.User.Tests.Unit.Application.User;

public sealed class FakeUserDistributedCacheStorage(IMemoryCache cache) : InMemoryDistributedCacheStorageBase<UserEntity, Guid>(cache), IUserDistributedCacheStorage
{
}
