using NetSpace.Community.Domain;

namespace NetSpace.Community.UseCases;

public interface IRepository<TEntity, TId> : IReadonlyRepository<TEntity, TId>, IWriteonlyRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
