using FluentAssertions;
using NetSpace.User.Application.UserPost.Commands;
using NetSpace.User.Application.UserPost.Exceptions;
using NetSpace.User.Tests.Unit.Initializer;

namespace NetSpace.User.Tests.Unit.Application.UserPost.Commands;

public sealed class PartiallyUpdateUserPostCommandTests
{
    [Fact]
    public async Task Should_ReturnUpdatedUserPostResponse()
    {
        #region Arrange

        var testUsers = TestInitializer.Create3Users();
        var uof = await TestInitializer.CreateUnitOfWorkAsync();
        await uof.UserPosts.AddAsync(new Domain.UserPost.UserPostEntity
        { Body = "Testbody", Title = "testTitle", User = testUsers[0], UserId = testUsers[0].Id });

        await uof.Users.AddRangeAsync(testUsers);
        await uof.SaveChangesAsync();

        var command = new PartiallyUpdateUserPostCommand { Id = 1, Body = "newBody", Title = "newTitle" };
        var handler = new PartiallyUpdateUserPostCommandHandler(uof, new PartiallyUpdateUserPostCommandValidator(), TestMapper.Create());
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
        await uof.UserPosts.AddAsync(new Domain.UserPost.UserPostEntity
        { Body = "Testbody", Title = "testTitle", User = testUsers[0], UserId = testUsers[0].Id });

        await uof.Users.AddRangeAsync(testUsers);
        await uof.SaveChangesAsync();

        var command = new PartiallyUpdateUserPostCommand { Id = 23, Body = "newBody", Title = "newTitle" };
        var handler = new PartiallyUpdateUserPostCommandHandler(uof, new PartiallyUpdateUserPostCommandValidator(), TestMapper.Create());
        #endregion

        #region Act

        await Assert.ThrowsAsync<UserPostNotFoundException>(async () =>
        {
            var result = await handler.Handle(command, CancellationToken.None);
        });
        #endregion
    }
}
