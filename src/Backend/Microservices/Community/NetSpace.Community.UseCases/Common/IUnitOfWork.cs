using NetSpace.Community.UseCases.Community;
using NetSpace.Community.UseCases.CommunityPost;
using NetSpace.Community.UseCases.CommunityPostUserComment;
using NetSpace.Community.UseCases.CommunitySubscription;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.UseCases.Common;

public interface IUnitOfWork
{
    public IUserRepository Users { get; }
    public ICommunityRepository Communities { get; }
    public ICommunityPostRepository CommunityPosts { get; }
    public ICommunityPostUserCommentRepository CommunityPostUserComments { get; }
    public ICommunitySubscriptionRepository CommunitySubscriptions { get; }


    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
