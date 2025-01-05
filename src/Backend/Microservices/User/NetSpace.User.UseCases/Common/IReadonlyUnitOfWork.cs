using NetSpace.User.UseCases.User;
using NetSpace.User.UseCases.UserPost;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.UseCases.Common;

public interface IReadonlyUnitOfWork
{
    public IUserReadonlyRepository Users { get; }
    public IUserPostReadonlyRepository UserPosts { get; }
    public IUserPostUserCommentReadonlyRepository UserPostUserComments { get; }
}
