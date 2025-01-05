using NetSpace.User.Domain.UserPostUserComment;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.Common.Repositories;

namespace NetSpace.User.UseCases.UserPostUserComment;

public interface IUserPostUserCommentReadonlyRepository : IReadonlyRepository<UserPostUserCommentEntity, int>
{
    public Task<IEnumerable<UserPostUserCommentEntity>> FilterAsync(UserPostUserCommentFilterOptions filter,
                                                    PaginationOptions pagination,
                                                    SortOptions sort,
                                                    CancellationToken cancellationToken = default);
}
