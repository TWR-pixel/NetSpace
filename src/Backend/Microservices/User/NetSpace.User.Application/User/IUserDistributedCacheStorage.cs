using NetSpace.User.Application.Common.Cache;
using NetSpace.User.Domain.User;

namespace NetSpace.User.Application.User;

public interface IUserDistributedCacheStorage : IDistributedCacheStorage<UserEntity, Guid>
{
}
