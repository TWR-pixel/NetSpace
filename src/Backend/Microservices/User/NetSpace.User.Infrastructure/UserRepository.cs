using NetSpace.User.Domain;
using NetSpace.User.UseCases;

namespace NetSpace.User.Infrastructure;

public sealed class UserRepository : IUserRepository
{
    public Task<UserEntity> AddAsync(UserEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserEntity>> AddRangeAsync(IEnumerable<UserEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(UserEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task DeleteRangeAsync(IEnumerable<UserEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<UserEntity?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserEntity>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(UserEntity entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task UpdateRangeAsync(IEnumerable<UserEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
