using NetSpace.Community.Domain.Community;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.UseCases.Community;

public interface ICommunityReadonlyRepository : IReadonlyRepository<CommunityEntity, int>
{
    public Task<IEnumerable<CommunityEntity>?> FilterAsync(CommunityFilterOptions options, PaginationOptions pagination, SortOptions sort, CancellationToken cancellationToken = default);
    public Task<CommunityEntity?> GetByIdWithDetails(int id, CancellationToken cancellationToken = default);
}
