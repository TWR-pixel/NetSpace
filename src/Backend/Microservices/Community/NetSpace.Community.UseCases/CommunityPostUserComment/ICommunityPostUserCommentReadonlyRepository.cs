using NetSpace.Community.Domain.CommunityPostUserComment;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.UseCases.CommunityPostUserComment;

public interface ICommunityPostUserCommentReadonlyRepository : IReadonlyRepository<CommunityPostUserCommentEntity, int>
{
    public Task<IEnumerable<CommunityPostUserCommentEntity>?> FilterAsync(CommunityPostUsercommentFilterOptions filter,
                                                                      PaginationOptions pagination,
                                                                      SortOptions sort,
                                                                      CancellationToken cancellationToken = default);
}
