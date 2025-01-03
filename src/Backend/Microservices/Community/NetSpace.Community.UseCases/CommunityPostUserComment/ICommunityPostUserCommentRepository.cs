using NetSpace.Community.Domain.CommunityPostUserComment;

namespace NetSpace.Community.UseCases.CommunityPostUserComment;

public interface ICommunityPostUserCommentRepository : IRepository<CommunityPostUserCommentEntity, int>
{
    public Task<IEnumerable<CommunityPostUserCommentEntity>?> FilterAsync(CommunityPostUsercommentFilterOptions filter,
                                                                          PaginationOptions pagination,
                                                                          SortOptions sort,
                                                                          CancellationToken cancellationToken = default);
}
