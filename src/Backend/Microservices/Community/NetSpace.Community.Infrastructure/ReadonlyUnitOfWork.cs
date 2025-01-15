using NetSpace.Common.Injector;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.UseCases.Community;
using NetSpace.Community.UseCases.CommunityPost;
using NetSpace.Community.UseCases.CommunityPostUserComment;
using NetSpace.Community.UseCases.CommunitySubscription;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Infrastructure;

[Inject(ImplementationsFor = [typeof(IReadonlyUnitOfWork)])]
public sealed class ReadonlyUnitOfWork(IUserReadonlyRepository users,
                                       ICommunityReadonlyRepository communities,
                                       ICommunityPostReadonlyRepository communityPosts,
                                       ICommunityPostUserCommentReadonlyRepository communityPostUserComments,
                                       ICommunitySubscriptionReadonlyRepository communitySubscriptions) : IReadonlyUnitOfWork
{
    public IUserReadonlyRepository Users => users;
    public ICommunityReadonlyRepository Communities => communities;
    public ICommunityPostReadonlyRepository CommunityPosts => communityPosts;
    public ICommunityPostUserCommentReadonlyRepository CommunityPostUserComments => communityPostUserComments;
    public ICommunitySubscriptionReadonlyRepository CommunitySubscriptions => communitySubscriptions;
}
