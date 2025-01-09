using Microsoft.EntityFrameworkCore;
using NetSpace.Community.Domain.Community;
using NetSpace.Community.Domain.CommunityPost;
using NetSpace.Community.Domain.CommunityPostUserComment;
using NetSpace.Community.Domain.User;
using NetSpace.Community.Infrastructure;

namespace NetSpace.Community.Tests.Unit.TestInitializer;

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
            new() {Name = "Name1", OwnerId = users[0].Id, Owner = users[0]}
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

    public static async Task<UnitOfWork> CreateUnitOfWorkAsync()
    {
        var dbContext = CreateInMemoryDb();
        var userRepo = new UserRepository(dbContext);
        var userPostUserCommentRepo = new UserPostUserCommentRepository(dbContext);
        var UserPostRepo = new UserPostRepository(dbContext);

        var testUsers = Create3Users();
        var testPosts = Create3UserPosts(testUsers);
        var testPostComments = Create3UserPostUserComments(testPosts, testUsers);

        var uof = new UnitOfWork(userRepo, UserPostRepo, userPostUserCommentRepo, dbContext);

        return uof;
    }

    public static async Task<ReadonlyUnitOfWork> CreateReadonlyUnitOfWorkAsync()
    {
        var dbContext = CreateInMemoryDb();

        var userRepo = new UserRepository(dbContext);
        var userPostUserCommentRepo = new UserPostUserCommentRepository(dbContext);
        var UserPostRepo = new UserPostRepository(dbContext);

        var testUsers = Create3Users();
        var testPosts = Create3UserPosts(testUsers);
        var testPostComments = Create3UserPostUserComments(testPosts, testUsers);

        var uof = new ReadonlyUnitOfWork(userRepo, UserPostRepo, userPostUserCommentRepo);

        //await uof.Users.AddRangeAsync(testUsers);
        //await uof.UserPosts.AddRangeAsync(testPosts);
        //await uof.UserPostUserComments.AddRangeAsync(testPostComments);

        return uof;
    }

    public static async Task<ReadonlyUnitOfWork> CreateReadonlyUnitOfWorkWithUserAndUserPostsAndUserPostUserCommentsAsync()
    {
        var dbContext = CreateInMemoryDb();

        var userRepo = new UserRepository(dbContext);
        var userPostUserCommentRepo = new UserPostUserCommentRepository(dbContext);
        var UserPostRepo = new UserPostRepository(dbContext);

        var testUsers = Create3Users();
        var testPosts = Create3UserPosts(testUsers);
        var testPostComments = Create3UserPostUserComments(testPosts, testUsers);

        await dbContext.Users.AddRangeAsync(testUsers);
        await dbContext.UserPosts.AddRangeAsync(testPosts);
        await dbContext.UserPostUserComments.AddRangeAsync(testPostComments);
        await dbContext.SaveChangesAsync();

        var uof = new ReadonlyUnitOfWork(userRepo, UserPostRepo, userPostUserCommentRepo);

        return uof;
    }
}
