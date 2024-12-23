using NetSpace.Common.Domain;

namespace NetSpace.Common.UseCases;

public interface IRepository<TEntity, TId> : IReadonlyRepository<TEntity, TId>, IWriteonlyRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
}
