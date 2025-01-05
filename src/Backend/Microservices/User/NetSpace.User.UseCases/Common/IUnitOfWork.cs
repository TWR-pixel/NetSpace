using NetSpace.User.UseCases.User;
using NetSpace.User.UseCases.UserPost;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.UseCases.Common;

public interface IUnitOfWork
{
    public IUserRepository Users { get; }
    public IUserPostRepository UserPosts { get; }
    public IUserPostUserCommentRepository UserPostUserComments { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
