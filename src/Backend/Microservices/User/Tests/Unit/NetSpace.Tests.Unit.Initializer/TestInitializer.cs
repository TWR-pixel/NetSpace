using Microsoft.EntityFrameworkCore;
using NetSpace.User.Domain.User;
using NetSpace.User.Domain.UserPost;
using NetSpace.User.Domain.UserPostUserComment;
using NetSpace.User.Infrastructure;
using NetSpace.User.Infrastructure.User;
using NetSpace.User.Infrastructure.UserPost;
using NetSpace.User.Infrastructure.UserPostUserComment;

namespace NetSpace.Tests.Unit.Initializer;

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
            new(){Nickname = "nickname1", Name = "name1", Surname = "surname1", Email = "email1@mail.ru"},
            new(){Nickname = "nickname2", Name = "name2", Surname = "surname2", Email = "email2@mail.ru"},
            new(){Nickname = "nickname3", Name = "name3", Surname = "surname3", Email = "email3@mail.ru"},
        };

        return users;
    }

    public static List<UserPostEntity> Creat3UserPosts(IEnumerable<UserEntity> users)
    {
        var userPosts = new List<UserPostEntity>
        {
            new(){Body = "TestBody", Title = "TestTitle", User = users.First()},
            new(){Body = "TestBody", Title = "TestTitle", User = users.Last()},
            new(){Body = "TestBody", Title = "TestTitle", User = users.Last()}
        };

        return userPosts;
    }

    public static List<UserPostUserCommentEntity> Create3UserPostUserComments(IEnumerable<UserPostEntity> userPosts, IEnumerable<UserEntity> users)
    {
        var userComments = new List<UserPostUserCommentEntity>
        {
            new() { Body = "TestBody", Owner = users.First() },
            new() { Body = "TestBody", Owner = users.Last() },
            new() { Body = "TestBody", Owner = users.Last() }
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
        var testPosts = Creat3UserPosts(testUsers);
        var testPostComments = Create3UserPostUserComments(testPosts, testUsers);

        var uof = new UnitOfWork(userRepo, UserPostRepo, userPostUserCommentRepo, dbContext);

        //await uof.Users.AddRangeAsync(testUsers);
        //await uof.UserPosts.AddRangeAsync(testPosts);
        //await uof.UserPostUserComments.AddRangeAsync(testPostComments);

        return uof;
    }

    public static async Task<ReadonlyUnitOfWork> CreateReadonlyUnitOfWorkAsync()
    {
        var dbContext = CreateInMemoryDb();

        var userRepo = new UserRepository(dbContext);
        var userPostUserCommentRepo = new UserPostUserCommentRepository(dbContext);
        var UserPostRepo = new UserPostRepository(dbContext);

        var testUsers = Create3Users();
        var testPosts = Creat3UserPosts(testUsers);
        var testPostComments = Create3UserPostUserComments(testPosts, testUsers);

        var uof = new ReadonlyUnitOfWork(userRepo, UserPostRepo, userPostUserCommentRepo);

        //await uof.Users.AddRangeAsync(testUsers);
        //await uof.UserPosts.AddRangeAsync(testPosts);
        //await uof.UserPostUserComments.AddRangeAsync(testPostComments);

        return uof;
    }

    public static async Task<ReadonlyUnitOfWork> CreateReadonlyUnitOfWorkWithUserAsync()
    {
        var dbContext = CreateInMemoryDb();

        await dbContext.Users.AddAsync(Create3Users().First());
        await dbContext.SaveChangesAsync();

        var userRepo = new UserRepository(dbContext);
        var userPostUserCommentRepo = new UserPostUserCommentRepository(dbContext);
        var UserPostRepo = new UserPostRepository(dbContext);

        var testUsers = Create3Users();
        var testPosts = Creat3UserPosts(testUsers);
        var testPostComments = Create3UserPostUserComments(testPosts, testUsers);

        var uof = new ReadonlyUnitOfWork(userRepo, UserPostRepo, userPostUserCommentRepo);

        //await uof.Users.AddRangeAsync(testUsers);
        //await uof.UserPosts.AddRangeAsync(testPosts);
        //await uof.UserPostUserComments.AddRangeAsync(testPostComments);

        return uof;
    }
}
