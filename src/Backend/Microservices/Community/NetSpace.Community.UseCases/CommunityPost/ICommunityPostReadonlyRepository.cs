using NetSpace.Community.Domain.CommunityPost;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.UseCases.CommunityPost;

public interface ICommunityPostReadonlyRepository : IReadonlyRepository<CommunityPostEntity, int>
{
    public Task<IEnumerable<CommunityPostEntity>> FilterAsync(CommunityPostFilterOptions filter,
                                                                PaginationOptions pagination,
                                                                SortOptions sort,
                                                                CancellationToken cancellationToken = default);
    public Task<CommunityPostEntity?> GetByIdWithDetails(int id, CancellationToken cancellationToken = default);    
}
