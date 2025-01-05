using Neo4jClient;
using NetSpace.Friendship.Domain;
using NetSpace.Friendship.Domain.User;
using NetSpace.Friendship.UseCases.Friendship;

namespace NetSpace.Friendship.Infrastructure.Friendship;

public sealed class FriendshipRepository(IGraphClient client) : IFriendshipRepository
{
    public async Task CreateFriendship(UserEntity from, UserEntity to, FriendshipStatus status, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await client.Cypher
            .Match("(fromEntity:UserEntity { Id: $fromId}), (toEntity:UserEntity {Id: $toId}) CREATE (fromEntity)-[:FRIENDS_WITH {status: $status}]->(toEntity)")
            .WithParam("fromId", from.Id)
            .WithParam("toId", to.Id)
            .WithParam("status", status)
            .ExecuteWithoutResultsAsync();
    }

    public async Task<long> FriendsCountById(UserEntity from, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await client.Cypher
            .Match("(user:UserEntity {Id: $id})-[:FRIENDS_WITH]->(users:UserEntity)")
            .WithParam("id", from.Id)
            .Return(users => users.Count())
            .ResultsAsync;

        return result.First();
    }

    public async Task<long> FollowersCountById(UserEntity from, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await client.Cypher
            .Match("(user:UserEntity {Id: $id})<-[:FRIENDS_WITH]-(followers:UserEntity)")
            .WithParam("id", from.Id)
            .Return(followers => followers.Count())
            .ResultsAsync;

        return result.First();
    }

    public async Task<IEnumerable<UserEntity>> GetAllFollowersByStatus(UserEntity from, FriendshipStatus status, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await client.Cypher
            .Match("(user:UserEntity {Id: $id })<-[:FRIENDS_WITH {status: $status}]-(all)")
            .WithParam("id", from.Id)
            .WithParam("status", status)
            .Return(all => all.As<UserEntity>())
            .ResultsAsync;

        return result;
    }

    public async Task<IEnumerable<UserEntity>> GetAllFriendsByStatus(UserEntity from, FriendshipStatus status, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await client.Cypher
            .Match("(us:UserEntity{Id: $id })-[:FRIENDS_WITH {status: $status }]->(u) ")
            .WithParam("id", from.Id)
            .WithParam("status", status)
            .Return(u => u.As<UserEntity>())
            .ResultsAsync;

        return result;
    }

    public async Task UpdateFriendshipStatus(UserEntity from, UserEntity to, FriendshipStatus status, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        await client.Cypher
            .Match("(fromUser:UserEntity {Id: $fromId })-[r:FRIENDS_WITH]-(toUser:UserEntity {Id: $toId}) SET r.status = $status")
            .WithParam("fromId", from.Id)
            .WithParam("toId", to.Id)
            .WithParam("status", status)
            .ExecuteWithoutResultsAsync();
    }
    public async Task<bool> ExistsFriendshipWithStatus(UserEntity from, UserEntity to, FriendshipStatus status, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await client.Cypher
            .Match("MATCH(p:UserEntity{Id: $fromId}), (b:UserEntity {Id: $toId}),(p)-[:FRIENDS_WITH]->(b) RETURN b")
            .WithParam("fromId", from.Id)
            .WithParam("toId", to.Id)
            .Return(b => b.As<UserEntity>())
            .ResultsAsync;

        if (result is null)
            return false;

        return true;
    }

    public async Task<IEnumerable<UserEntity>> GetPossibleFriends(UserEntity from, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var result = await client.Cypher
            .Match("(from:UserEntity{Id: $id})")
            .Match("(from)-[:FRIENDS_WITH*2]-(to)")
            .Where("NOT (from)-[:FRIEND_WITH]-(to)")
            .Return(to => to.As<UserEntity>())
            .Limit(10)
            .ResultsAsync;

        return result;
    }
}
