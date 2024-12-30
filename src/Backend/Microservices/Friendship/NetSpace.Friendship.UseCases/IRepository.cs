using NetSpace.Friendship.Domain;

namespace NetSpace.Friendship.UseCases;

public interface IRepository<TEntity, TId> : IReadonlyRepository<TEntity, TId>, IWriteonlyRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
}
