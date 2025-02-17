﻿using NetSpace.User.Domain;

namespace NetSpace.User.UseCases.Common.Repositories;

public interface IWriteonlyRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}
