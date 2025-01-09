using FluentAssertions;
using MassTransit.Testing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NetSpace.Tests.Unit.Initializer;
using NetSpace.User.Application.User;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Application.UserPost.Commands;
using NetSpace.User.Application.UserPost.Exceptions;

namespace NetSpace.Tests.Unit.Application.UserPost.Commands;

public sealed class DeleteUserPostByIdCommandTests
{
    [Fact]
    public async Task Should_ReturnDeletedUserPostResponse()
    {
        #region Arrange

        var testUsers = TestInitializer.Create3Users();
        var uof = await TestInitializer.CreateUnitOfWorkAsync();
        await uof.UserPosts.AddAsync(new NetSpace.User.Domain.UserPost.UserPostEntity
        { Body = "Testbody", Title = "testTitle", User = testUsers[0], UserId = testUsers[0].Id });

        await uof.Users.AddRangeAsync(testUsers);
        await uof.SaveChangesAsync();

        var command = new DeleteUserPostByIdCommand { Id = 1 };
        var memCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        var testCache = new FakeUserPostDistributedCacheStorage(memCache);
        var handler = new DeleteUserPostByIdCommandHandler(uof, TestMapper.Create(), testCache);
        #endregion

        #region Act
        var result = await handler.Handle(command, CancellationToken.None);
        #endregion

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_ThrowUserPostNotFoundException()
    {
        #region Arrange

        var testUsers = TestInitializer.Create3Users();
        var uof = await TestInitializer.CreateUnitOfWorkAsync();
        await uof.UserPosts.AddAsync(new NetSpace.User.Domain.UserPost.UserPostEntity
        { Body = "Testbody", Title = "testTitle", User = testUsers[0], UserId = testUsers[0].Id });

        await uof.Users.AddRangeAsync(testUsers);
        await uof.SaveChangesAsync();

        var command = new DeleteUserPostByIdCommand { Id = 24 };
        var memCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        var testCache = new FakeUserPostDistributedCacheStorage(memCache);
        var handler = new DeleteUserPostByIdCommandHandler(uof, TestMapper.Create(), testCache);
        #endregion

        #region Act

        await Assert.ThrowsAsync<UserPostNotFoundException>(async () =>
        {
            var result = await handler.Handle(command, CancellationToken.None);
        });
        #endregion
    }
}
