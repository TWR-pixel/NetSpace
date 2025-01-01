using NetSpace.User.Domain.User;

namespace NetSpace.User.Application.Common.Cache;

public interface IUserDistributedCacheStorage : IDistributedCacheStorage<UserEntity, string>
{
    public Task<UserEntity?> GetByEmailAsync(string email, CancellationToken cancellationToken = default);
}
