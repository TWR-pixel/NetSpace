﻿using Neo4jClient;
using NetSpace.Friendship.Domain;
using NetSpace.Friendship.UseCases;

namespace NetSpace.Friendship.Infrastructure.User;

public sealed class UserRepository(IGraphClient client) : IUserRepository
{
    public async Task<UserEntity> AddAsync(UserEntity entity, CancellationToken cancellationToken = default)
    {
        await client.Cypher.Create("(u:UserEntity $entity)")
            .WithParam("entity", entity)
            .ExecuteWithoutResultsAsync();

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
