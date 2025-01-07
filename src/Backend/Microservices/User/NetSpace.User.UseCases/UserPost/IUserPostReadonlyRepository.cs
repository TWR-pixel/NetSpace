using NetSpace.User.Domain.UserPost;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.Common.Repositories;

namespace NetSpace.User.UseCases.UserPost;

public interface IUserPostReadonlyRepository : IReadonlyRepository<UserPostEntity, int>
{

    public Task<IEnumerable<UserPostEntity>> FilterAsync(UserPostFilterOptions filter,
                                                         PaginationOptions paginationk,
                                                         SortOptions sort,
                                                         CancellationToken cancellationToken = default);

    public Task<UserPostEntity?> GetByIdWithDetails(int id, CancellationToken cancellationToken = default);
    public Task<IEnumerable<UserPostEntity>> GetLatest(PaginationOptions pagination, CancellationToken cancellationToken = default);
}
