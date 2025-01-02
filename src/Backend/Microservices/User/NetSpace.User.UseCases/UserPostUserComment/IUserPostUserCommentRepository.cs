using NetSpace.User.Domain.User;

namespace NetSpace.User.UseCases.UserPostUserComment;

public interface IUserPostUserCommentRepository : IRepository<UserPostUserCommentEntity, int>
{
    public Task<IEnumerable<UserPostUserCommentEntity>> Filter(UserPostUserCommentFilterOptions filter,
                                                        PaginationOptions pagination,
                                                        SortOptions sort,
                                                        CancellationToken cancellationToken = default);
}
