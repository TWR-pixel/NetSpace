using Microsoft.EntityFrameworkCore;
using NetSpace.Community.Domain.Community;
using NetSpace.Community.Domain.CommunityPost;
using NetSpace.Community.Domain.CommunityPostUserComment;
using NetSpace.Community.Domain.CommunitySubscription;
using NetSpace.Community.Domain.User;
using NetSpace.Community.Infrastructure;
using NetSpace.Community.Infrastructure.Community;
using NetSpace.Community.Infrastructure.CommunityPost;
using NetSpace.Community.Infrastructure.CommunityPostUserComment;
using NetSpace.Community.Infrastructure.CommunitySubscription;
using NetSpace.Community.Infrastructure.User;

namespace NetSpace.Community.Tests.Unit.Initializer;

public static class TestInitializer
{
    private static NetSpaceDbContext CreateInMemoryDb()
    {
        var options = new DbContextOptionsBuilder<NetSpaceDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var db = new NetSpaceDbContext(options);

        return db;
    }

    public static List<UserEntity> Create3Users()
    {
        var users = new List<UserEntity>
        {
            new(){Nickname = "nickname1", UserName = "name1", Surname = "surname1", Email = "email1@mail.ru"},
            new(){Nickname = "nickname2", UserName = "name2", Surname = "surname2", Email = "email2@mail.ru"},
            new(){Nickname = "nickname3", UserName = "name3", Surname = "surname3", Email = "email3@mail.ru"},
        };

        return users;
    }

    public static List<CommunityEntity> Create3Communities(List<UserEntity> users)
    {
        var communities = new List<CommunityEntity>()
        {
            new() {Name = "Name1", OwnerId = users[0].Id, Owner = users[0]},
            new() {Name = "Name2", OwnerId = users[1].Id, Owner = users[1]},
            new() {Name = "Name3", OwnerId = users[2].Id, Owner = users[2]}
        };

        return communities;
    }

    public static List<CommunityPostEntity> Create3CommunityPosts(IEnumerable<CommunityEntity> communities)
    {
        var userPosts = new List<CommunityPostEntity>
        {
            new(){Body = "TestBody", Title = "TestTitle1",  Community = communities.First()},
            new(){Body = "TestBody", Title = "TestTitle2", Community = communities.Last()},
            new(){Body = "TestBody", Title = "TestTitle3", Community = communities.Last()}
        };

        return userPosts;
    }

    public static List<CommunityPostUserCommentEntity> Create3CommunityPostsUserComments(IEnumerable<CommunityPostEntity> communityPosts, IEnumerable<UserEntity> users)
    {
        var userComments = new List<CommunityPostUserCommentEntity>
        {
            new() { Body = "TestBody1", Owner = users.First() },
            new() { Body = "TestBody2", Owner = users.Last() },
            new() { Body = "TestBody3", Owner = users.Last() }
        };

        return userComments;
    }
    public static List<CommunitySubscriptionEntity> Create3CommunitySubscriptions(IEnumerable<CommunityEntity> communities, IEnumerable<UserEntity> users)
    {
        var subscriptions = new List<CommunitySubscriptionEntity>
        {
            new() { Community = communities.First() , Subscriber = users.First()},
            new() { Community = communities.Last() , Subscriber = users.Last() },
            new() { Community = communities.First() , Subscriber = users.First() }
        };

        return subscriptions;
    }


    public static async Task<UnitOfWork> CreateUnitOfWorkWithTestDataAsync()
    {
        var dbContext = CreateInMemoryDb();
        var userRepo = new UserRepository(dbContext);
        var communityRepo = new CommunityRepository(dbContext);
        var communityPostsRepo = new CommunityPostRepository(dbContext);
        var communityPostUserCommentRepository = new CommunityPostUserCommentRepository(dbContext);
        var communitySubscriptionRepository = new CommunitySubscriptionRepository(dbContext);

        var testUsers = Create3Users();
        var testCommunities = Create3Communities(testUsers);
        var testPostCommunities = Create3CommunityPosts(testCommunities);
        var testPostComments = Create3CommunityPostsUserComments(testPostCommunities, testUsers);
        var testSubscriptions = Create3CommunitySubscriptions(testCommunities, testUsers);


        var uof = new UnitOfWork(userRepo, communityRepo, communityPostsRepo, communityPostUserCommentRepository, communitySubscriptionRepository, dbContext);

        await uof.Users.AddRangeAsync(testUsers);
        await uof.Communities.AddRangeAsync(testCommunities);
        await uof.CommunityPosts.AddRangeAsync(testPostCommunities);
        await uof.CommunityPostUserComments.AddRangeAsync(testPostComments);
        await uof.CommunitySubscriptions.AddRangeAsync(testSubscriptions);
        await uof.SaveChangesAsync();

        return uof;
    }

    public static async Task<ReadonlyUnitOfWork> CreateReadonlyUnitOfWorkWithUserAndCommunitiesTestDataAsync()
    {
        var dbContext = CreateInMemoryDb();

        var userRepo = new UserRepository(dbContext);
        var communityRepo = new CommunityRepository(dbContext);
        var communityPostsRepo = new CommunityPostRepository(dbContext);
        var communityPostUserCommentRepository = new CommunityPostUserCommentRepository(dbContext);
        var communitySubscriptionRepository = new CommunitySubscriptionRepository(dbContext);

        var testUsers = Create3Users();
        var testCommunities = Create3Communities(testUsers);
        var testPostCommunities = Create3CommunityPosts(testCommunities);
        var testPostComments = Create3CommunityPostsUserComments(testPostCommunities, testUsers);
        var testSubscriptions = Create3CommunitySubscriptions(testCommunities, testUsers);

        await dbContext.Users.AddRangeAsync(testUsers);
        await dbContext.Communities.AddRangeAsync(testCommunities);
        await dbContext.CommunityPosts.AddRangeAsync(testPostCommunities);
        await dbContext.CommunityPostUserComments.AddRangeAsync(testPostComments);
        await dbContext.CommunitySubscriptions.AddRangeAsync(testSubscriptions);
        await dbContext.SaveChangesAsync();

        var uof = new ReadonlyUnitOfWork(userRepo, communityRepo, communityPostsRepo, communityPostUserCommentRepository, communitySubscriptionRepository);

        return uof;
    }
}
