using NetSpace.Community.Domain.CommunityPost;

namespace NetSpace.Community.UseCases.CommunityPost;

public interface ICommunityPostRepository : IRepository<CommunityPostEntity, int>
{
    public Task<IEnumerable<CommunityPostEntity>> FilterAsync(CommunityPostFilterOptions filter,
                                                                    PaginationOptions pagination,
                                                                    SortOptions sort,
                                                                    CancellationToken cancellationToken = default);
}
