using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.User;
using NetSpace.User.UseCases.UserPost;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.Infrastructure;

public sealed class ReadonlyUnitOfWork(IUserReadonlyRepository users, IUserPostReadonlyRepository userPosts, IUserPostUserCommentReadonlyRepository userPostUserComments) : IReadonlyUnitOfWork
{
    public IUserReadonlyRepository Users => users;
    public IUserPostReadonlyRepository UserPosts => userPosts;
    public IUserPostUserCommentReadonlyRepository UserPostUserComments => userPostUserComments;
}
