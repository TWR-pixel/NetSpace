using Microsoft.AspNetCore.Identity;
using NetSpace.Identity.Domain;

namespace NetSpace.Identity.UseCases;

public interface IWriteonlyRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
    where TId : notnull
{
    public Task<IdentityResult> AddAsync(TEntity entity, string password, CancellationToken cancellationToken = default);
    public Task UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
    public Task DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}
