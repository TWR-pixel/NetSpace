using Neo4jClient;
using NetSpace.Friendship.Domain.User;
using NetSpace.Friendship.UseCases.User;

namespace NetSpace.Friendship.Infrastructure.User;

public sealed class UserRepository(IGraphClient client) : IUserRepository
{
    public async Task<UserEntity> AddAsync(UserEntity entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await client.Cypher.Create("(user:UserEntity $entity)")
            .WithParam("entity", entity)
            .Return(user => user.As<UserEntity>())
            .ResultsAsync;

        return result.First();
    }

    public Task<bool> AnyAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(UserEntity entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await client.Cypher
            .Match("(user:UserEntity { Id: $id }) DELETE user")
            .ExecuteWithoutResultsAsync();
    }

    public async Task<UserEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await client.Cypher
            .Match("(user:UserEntity { Id: $id })")
            .Return(user => user.As<UserEntity>())
            .ResultsAsync;

        return result.FirstOrDefault();
    }

    public async Task<IEnumerable<UserEntity>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await client.Cypher
            .Match("(n:UserEntity)")
            .Return(n => n.As<UserEntity>())
            .ResultsAsync;

        return result;
    }

    public async Task UpdateAsync(UserEntity entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await client.Cypher
            .Match("(user:UserEntity {Id: $id})")
            .WithParam("id", entity.Id)
            .Set("user = $entity")
            .WithParam("entity", entity)
            .ExecuteWithoutResultsAsync();
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

}
