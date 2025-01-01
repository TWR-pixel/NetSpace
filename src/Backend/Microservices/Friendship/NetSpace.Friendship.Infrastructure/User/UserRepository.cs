using Neo4jClient;
using NetSpace.Friendship.Domain;
using NetSpace.Friendship.UseCases;

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

    public async Task<long> FriendsCountById(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await client.Cypher
            .Match("(user:UserEntity {Id: $id})-[:FRIEND_WITH]->(users:UserEntity)")
            .Return(users => users.Count())
            .ResultsAsync;

        return result.First();
    }

    public async Task<long> FollowersCountById(Guid id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await client.Cypher
            .Match("(user:UserEntity {Id: $id})<-[:FRIEND_WITH]-(followers:UserEntity)")
            .Return(followers => followers.Count())
            .ResultsAsync;

        return result.First();
    }

    public async Task CreateFriendship(Guid fromId, Guid toId, FriendshipStatus status, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await client.Cypher
            .Match("(fromEntity:UserEntity { Id: $fromId}), (toEntity:UserEntity {Id: $toId}) CREATE (fromEntity)-[:FRIEND_WITH {status: $status}]->(toEntity)")
            .WithParam("fromId", fromId)
            .WithParam("toId", toId)
            .WithParam("status", status)
            .ExecuteWithoutResultsAsync();
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

    public async Task<IEnumerable<UserEntity>> GetAllFollowersByStatus(Guid id, FriendshipStatus status, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await client.Cypher
            .Match("(user:UserEntity {Id: $id })<-[:FRIEND_WITH {status: $status}]-(all)")
            .Return(all => all.As<UserEntity>())
            .ResultsAsync;

        return result;
    }

    public async Task<IEnumerable<UserEntity>> GetAllFriendsByStatus(Guid id, FriendshipStatus status, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await client.Cypher
            .Match("(us:UserEntity{Id: $id })-[:FRIEND_WITH {status: $status }]->(u) ")
            .WithParam("id", id)
            .WithParam("status", status)
            .Return(u => u.As<UserEntity>())
            .ResultsAsync;

        return result;
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

    public async Task UpdateFriendshipStatus(Guid fromId, Guid toId, FriendshipStatus status, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await client.Cypher
            .Match("(fromUser:UserEntity {Id: $fromId })-[r:FRIEND_WITH]-(toUser:UserEntity {Id: $toId}) SET r.status = $status")
            .WithParam("fromId", fromId)
            .WithParam("toId", toId)
            .WithParam("status", status)
            .ExecuteWithoutResultsAsync();
    }

    public Task<int> CountAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
