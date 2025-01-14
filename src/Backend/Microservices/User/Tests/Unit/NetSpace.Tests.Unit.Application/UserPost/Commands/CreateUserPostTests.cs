using FluentAssertions;
using FluentValidation;
using NetSpace.User.Application.UserPost.Commands;
using NetSpace.User.Tests.Unit.Initializer;

namespace NetSpace.User.Tests.Unit.Application.UserPost.Commands;

public sealed class CreateUserPostTests
{
    [Fact]
    public async Task Should_CreateUserPostWithValidParameters()
    {
        #region Arrange

        var testUsers = TestInitializer.Create3Users();
        var uof = await TestInitializer.CreateUnitOfWorkAsync();
        await uof.UserPosts.AddRangeAsync(TestInitializer.Create3UserPosts(testUsers));
        await uof.Users.AddRangeAsync(testUsers);
        await uof.SaveChangesAsync();
        var command = new CreateUserPostCommand { Body = "testBody", OwnerId = testUsers.First().Id, Title = "testTitle" };
        var handler = new CreateUserPostCommandHandler(uof, TestMapper.Create(), new CreateUserPostCommandValidator());
        #endregion

        #region Act
        var result = await handler.Handle(command, CancellationToken.None);
        #endregion

        #region Assert
        result.Should().NotBeNull();
        #endregion
    }

    [Fact]
    public async Task Should_ThrowValidationExceptionWithInvalidParameters()
    {
        #region Arrange

        var testUsers = TestInitializer.Create3Users();
        var uof = await TestInitializer.CreateUnitOfWorkAsync();
        await uof.Users.AddRangeAsync(testUsers);
        await uof.SaveChangesAsync();
        var command = new CreateUserPostCommand { Body = "  ", OwnerId = testUsers.First().Id, Title = " " };
        var handler = new CreateUserPostCommandHandler(uof, TestMapper.Create(), new CreateUserPostCommandValidator());
        #endregion

        #region Act

        await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(command, CancellationToken.None));
        #endregion

    }
}
