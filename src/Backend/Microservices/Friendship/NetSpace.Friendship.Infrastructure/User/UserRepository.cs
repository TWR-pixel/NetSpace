using NetSpace.Friendship.Domain;
using NetSpace.Friendship.UseCases;

namespace NetSpace.Friendship.Infrastructure.User;

public sealed class UserRepository(NetSpaceDbContext dbContext) : IUserRepository
{
    public Task<UserEntity> AddAsync(UserEntity entity, CancellationToken cancellationToken = default)
    {
        dbContext.Users.Add(entity);

        return Task.FromResult(entity);
    }

    public Task<IEnumerable<UserEntity>> AddRangeAsync(IEnumerable<UserEntity> entities, CancellationToken cancellationToken = default)
    {
        dbContext.Users.AddRange(entities);

        return Task.FromResult(entities);
    }

    public async Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Users.Match().AnyAsync(cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Users.Match().CountAsync(cancellationToken);
    }

    public Task DeleteAsync(UserEntity entity, CancellationToken cancellationToken = default)
    {
        dbContext.Database.Session.Run("MATCH (user:UserEntity) WHERE user.Id = \"{userId}\" DELETE user", entity.Id);

        return Task.CompletedTask;
    }

    public Task DeleteRangeAsync(IEnumerable<UserEntity> entities, CancellationToken cancellationToken = default)
    {
        dbContext.Users.Match().

        return Task.CompletedTask;
    }

    public async Task<UserEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await dbContext.Users.Match(u => u.Where(u => u.Id, id)).FirstOrDefaultAsync(cancellationToken);

        return entity;
    }

    public async Task<IEnumerable<UserEntity>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await dbContext.Users.Match().ToListAsync(cancellationToken);

        return entities;
    }

    public async Task UpdateAsync(UserEntity entity, CancellationToken cancellationToken = default)
    {

        await dbContext.Users.Match(u => u.Where(u => u.Id, entity.Id)).UpdateAsync(u => u.Set(entity), cancellationToken);
    }

    public async Task UpdateRangeAsync(IEnumerable<UserEntity> entities, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
