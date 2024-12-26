using Neo4jClient;
using NetSpace.Friendship.Domain;
using NetSpace.Friendship.UseCases;

namespace NetSpace.Friendship.Infrastructure.User;

public sealed class UserRepository(IGraphClient client) : IUserRepository
{
    public async Task<UserEntity> AddAsync(UserEntity entity, CancellationToken cancellationToken = default)
    {
        await client.Cypher.Create("(user:UserEntity $entity)")
            .WithParam("entity", entity)
            .ExecuteWithoutResultsAsync(); // Будут статусы для дружбы: ACCEPTED (Принята), (REJECTED) Отвергнута,(WAITING_FOR_CONFIRMATION) Ждет ответа

        return entity;
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

    public Task CreateFriendship(Guid fromId, Guid toId, CancellationToken cancellationToken = default)
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

    public Task<UserEntity?> FindByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserEntity>> GetAllAcceptedFriendsById(Guid id, CancellationToken cancellationToken = default)
    {
        //var query = "match (users:UserEntity{Id: $id })-[:FRIENDS_WITH {status: $friendship_status }]->(friends:UserEntity) ";

        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserEntity>?> GetAllAsync(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<UserEntity>> GetAllFriendsByStatus(Guid id, FriendshipStatus status, CancellationToken cancellationToken = default)
    {
        var result = await client.Cypher
            .Match("(us:UserEntity{Id: \"1a44aa32-4c26-41cc-8270-fcdc22e0f35f\" })-[:FRIEND_WITH {status: 1 }]->(u) ")
           // .WithParam("id", id)
            //.WithParam("status", status)
            .Return(u => u.As<UserEntity>())
            .ResultsAsync;

        Console.WriteLine(client.Cypher.Query.DebugQueryText);

        return result;
    }

    public Task<IEnumerable<UserEntity>> GetAllRejectedFriendsById(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<UserEntity>> GetAllWaitingForConfirmationFriendsById(Guid id, CancellationToken cancellationToken)
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
