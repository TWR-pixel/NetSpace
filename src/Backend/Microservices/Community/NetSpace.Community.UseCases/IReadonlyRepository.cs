using NetSpace.Community.Domain;

namespace NetSpace.Community.UseCases;

public interface IReadonlyRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    public Task<TEntity?> FindByIdAsync(TId id, CancellationToken cancellationToken = default);
    public Task<int> CountAsync(CancellationToken cancellationToken = default);
    public Task<bool> AnyAsync(CancellationToken cancellationToken = default);
    public Task<IEnumerable<TEntity>?> GetAllAsync(CancellationToken cancellationToken = default);
}
