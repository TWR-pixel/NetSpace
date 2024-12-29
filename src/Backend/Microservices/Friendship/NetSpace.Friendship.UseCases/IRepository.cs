using NetSpace.Common.Domain;

namespace NetSpace.Friendship.UseCases;

public interface IRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    public Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task<bool> AnyAsync(CancellationToken cancellationToken = default);

    public Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken = default);
    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task<IEnumerable<TEntity>?> GetAllAsync(CancellationToken cancellationToken = default);
    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
