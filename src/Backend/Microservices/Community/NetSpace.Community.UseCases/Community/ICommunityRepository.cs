using NetSpace.Community.Domain.Community;

namespace NetSpace.Community.UseCases.Community;

public interface ICommunityRepository : IRepository<CommunityEntity, int>
{
    public Task<IEnumerable<CommunityEntity>?> FilterAsync(CommunityFilterOptions options, PaginationOptions pagination, SortOptions sort, CancellationToken cancellationToken = default);
}
