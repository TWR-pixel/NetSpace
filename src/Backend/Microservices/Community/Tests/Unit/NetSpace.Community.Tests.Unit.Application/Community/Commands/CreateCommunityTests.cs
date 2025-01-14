using FluentAssertions;
using FluentValidation;
using NetSpace.Community.Application.Community.Commands;
using NetSpace.Community.Tests.Unit.Initializer;

namespace NetSpace.Community.Tests.Unit.Application.Community.Commands;

public sealed class CreateCommunityTests
{
    [Fact]
    public async Task Should_CreateCommunityWithValidParameters()
    {
        #region Arrange
        var uof = await TestInitializer.CreateUnitOfWorkWithTestDataAsync();
        var users = await uof.Users.GetAllAsync();
        var ownerId = users!.First().Id;
        var command = new CreateCommunityCommand { Name = "Name", OwnerId = ownerId };
        var handler = new CreateCommunityCommandHandler(uof, new CreateCommunityCommandValidator(), TestMapper.Create());
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
        var uof = await TestInitializer.CreateUnitOfWorkWithTestDataAsync();
        var users = await uof.Users.GetAllAsync();
        var ownerId = users!.First().Id;
        var command = new CreateCommunityCommand { Name = " ", OwnerId = ownerId };
        var handler = new CreateCommunityCommandHandler(uof, new CreateCommunityCommandValidator(), TestMapper.Create());
        #endregion

        #region Act

        await Assert.ThrowsAsync<ValidationException>(async () => await handler.Handle(command, CancellationToken.None));
        #endregion

    }
}
