using FluentAssertions;
using NetSpace.Community.Application.Community.Commands;
using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.Tests.Unit.Initializer;

namespace NetSpace.Community.Tests.Unit.Application.Community.Commands;

public sealed class UpdateCommunityTests
{
    [Fact]
    public async Task Should_ReturnUpdatedCommunityResponse()
    {
        #region Arrange
        var uof = await TestInitializer.CreateUnitOfWorkWithTestDataAsync();
        var users = await uof.Users.GetAllAsync();
        var ownerId = users!.First().Id;
        var command = new UpdateCommunityCommand { Id = 2, Name = "newName", OwnerId = ownerId };
        var handler = new UpdateCommunityCommandHandler(uof, TestMapper.Create(), new UpdateCommunityCommandValidator());
        #endregion

        #region Act
        var result = await handler.Handle(command, CancellationToken.None);
        #endregion

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_ThrowCommunityNotFoundException()
    {
        #region Arrange
        var uof = await TestInitializer.CreateUnitOfWorkWithTestDataAsync();
        var users = await uof.Users.GetAllAsync();
        var ownerId = users!.First().Id;
        var command = new UpdateCommunityCommand { Id = 22, Name = "newName", OwnerId = ownerId };
        var handler = new UpdateCommunityCommandHandler(uof, TestMapper.Create(), new UpdateCommunityCommandValidator());
        #endregion

        #region Act

        await Assert.ThrowsAsync<CommunityNotFoundException>(async () =>
        {
            var result = await handler.Handle(command, CancellationToken.None);
        });
        #endregion
    }
}
