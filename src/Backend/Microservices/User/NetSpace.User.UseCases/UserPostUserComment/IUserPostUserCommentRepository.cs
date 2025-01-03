using NetSpace.User.Domain.UserPostUserComment;

namespace NetSpace.User.UseCases.UserPostUserComment;

public interface IUserPostUserCommentRepository : IRepository<UserPostUserCommentEntity, int>
{
    public Task<IEnumerable<UserPostUserCommentEntity>> FilterAsync(UserPostUserCommentFilterOptions filter,
                                                        PaginationOptions pagination,
                                                        SortOptions sort,
                                                        CancellationToken cancellationToken = default);
}
