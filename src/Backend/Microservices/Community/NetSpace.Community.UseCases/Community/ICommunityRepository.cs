using NetSpace.Community.Domain;

namespace NetSpace.Community.UseCases.Community;

public interface ICommunityRepository : IRepository<CommunityEntity, int>
{
    public Task<CommunityEntity?> GetWithDetails(int id, CancellationToken cancellationToken = default);
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    public Task<IEnumerable<CommunityEntity>?> FilterAsync(FilterOptions options, CancellationToken cancellationToken = default);
}
