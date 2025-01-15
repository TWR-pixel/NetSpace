using NetSpace.Common.Injector;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.UseCases.Community;
using NetSpace.Community.UseCases.CommunityPost;
using NetSpace.Community.UseCases.CommunityPostUserComment;
using NetSpace.Community.UseCases.CommunitySubscription;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Infrastructure;

[Inject(ImplementationsFor = [typeof(IUnitOfWork)])]
public sealed class UnitOfWork(IUserRepository users,
                               ICommunityRepository communities,
                               ICommunityPostRepository communityPosts,
                               ICommunityPostUserCommentRepository communityPostUserComments,
                               ICommunitySubscriptionRepository communitySubscriptions,
                               NetSpaceDbContext dbContext) : IUnitOfWork
{
    public IUserRepository Users => users;
    public ICommunityRepository Communities => communities;
    public ICommunityPostRepository CommunityPosts => communityPosts;
    public ICommunityPostUserCommentRepository CommunityPostUserComments => communityPostUserComments;
    public ICommunitySubscriptionRepository CommunitySubscriptions => communitySubscriptions;

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.SaveChangesAsync(cancellationToken);
    }
}
