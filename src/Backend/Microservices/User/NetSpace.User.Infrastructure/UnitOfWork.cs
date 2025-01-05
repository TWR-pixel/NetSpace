using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.User;
using NetSpace.User.UseCases.UserPost;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.Infrastructure;

public sealed class UnitOfWork(IUserRepository users, IUserPostRepository userPosts, IUserPostUserCommentRepository userPostUserComments, NetSpaceDbContext dbContext) : IUnitOfWork
{
    public IUserRepository Users => users;
    public IUserPostRepository UserPosts => userPosts;
    public IUserPostUserCommentRepository UserPostUserComments => userPostUserComments;


    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
