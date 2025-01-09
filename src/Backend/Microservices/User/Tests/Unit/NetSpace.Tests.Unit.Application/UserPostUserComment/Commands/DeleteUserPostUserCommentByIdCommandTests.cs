using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NetSpace.Tests.Unit.Initializer;
using NetSpace.User.Application.UserPostUserComment.Commands;
using NetSpace.User.Application.UserPostUserComment.Exceptions;

namespace NetSpace.Tests.Unit.Application.UserPostUserComment.Commands;

public sealed class DeleteUserPostUserCommentByIdCommandTests
{
    [Fact]
    public async Task Should_ReturnDeletedUserPostUserCommentResponse()
    {
        #region Arrange

        var testUsers = TestInitializer.Create3Users();
        var testPosts = TestInitializer.Create3UserPosts(testUsers);
        var uof = await TestInitializer.CreateUnitOfWorkAsync();
        await uof.Users.AddRangeAsync(testUsers);
        await uof.UserPosts.AddRangeAsync(testPosts);
        await uof.UserPostUserComments.AddAsync(new NetSpace.User.Domain.UserPostUserComment
            .UserPostUserCommentEntity
        { Body = "testBody", Owner = testUsers[0], UserId = testUsers[0].Id, UserPost = testPosts[0], UserPostId = testPosts[0].Id });
        await uof.SaveChangesAsync();

        var memCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        var testCache = new FakeUserPostUserCommentDistributedCacheStorage(memCache);
        var command = new DeleteUserPostUserCommentByIdCommand { Id = 1 };
        var handler = new DeleteUserPostUserCommentCommandHandler(uof, TestMapper.Create(), testCache);
        #endregion

        #region Act
        var result = await handler.Handle(command, CancellationToken.None);
        #endregion

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_ThrowUserPostUserCommentNotFoundException()
    {
        #region Arrange

        var testUsers = TestInitializer.Create3Users();
        var testPosts = TestInitializer.Create3UserPosts(testUsers);
        var uof = await TestInitializer.CreateUnitOfWorkAsync();
        await uof.Users.AddRangeAsync(testUsers);
        await uof.UserPosts.AddRangeAsync(testPosts);
        await uof.UserPostUserComments.AddAsync(new NetSpace.User.Domain.UserPostUserComment
            .UserPostUserCommentEntity
        { Body = "testBody", Owner = testUsers[0], UserId = testUsers[0].Id, UserPost = testPosts[0], UserPostId = testPosts[0].Id });
        await uof.SaveChangesAsync();

        var memCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        var testCache = new FakeUserPostUserCommentDistributedCacheStorage(memCache);
        var command = new DeleteUserPostUserCommentByIdCommand { Id = 232 };
        var handler = new DeleteUserPostUserCommentCommandHandler(uof, TestMapper.Create(), testCache);
        #endregion

        #region Act

        await Assert.ThrowsAsync<UserPostUserCommentNotFoundException>(async () =>
        {
            var result = await handler.Handle(command, CancellationToken.None);
        });
        #endregion
    }
}
