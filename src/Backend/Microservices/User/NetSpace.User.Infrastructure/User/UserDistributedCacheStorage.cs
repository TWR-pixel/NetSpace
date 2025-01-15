using Microsoft.Extensions.Caching.Distributed;
using NetSpace.Common.Injector;
using NetSpace.User.Application.User;
using NetSpace.User.Domain.User;
using NetSpace.User.Infrastructure.Common.Cache;

namespace NetSpace.User.Infrastructure.User;


[Inject(ImplementationsFor = [typeof(IUserDistributedCacheStorage)])]
public sealed class UserDistributedCacheStorage(IDistributedCache cache) : DistributedCacheStorageBase<UserEntity, Guid>(cache), IUserDistributedCacheStorage
{
}
