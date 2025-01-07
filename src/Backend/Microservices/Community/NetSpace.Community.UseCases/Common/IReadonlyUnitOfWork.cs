using NetSpace.Community.UseCases.Community;
using NetSpace.Community.UseCases.CommunityPost;
using NetSpace.Community.UseCases.CommunityPostUserComment;
using NetSpace.Community.UseCases.CommunitySubscription;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.UseCases.Common;

public interface IReadonlyUnitOfWork
{
    public IUserReadonlyRepository Users { get; }
    public ICommunityReadonlyRepository Communities { get; }
    public ICommunityPostReadonlyRepository CommunityPosts { get; }
    public ICommunityPostUserCommentReadonlyRepository CommunityPostUserComments { get; }
    public ICommunitySubscriptionReadonlyRepository CommunitySubscriptions { get; }
}
