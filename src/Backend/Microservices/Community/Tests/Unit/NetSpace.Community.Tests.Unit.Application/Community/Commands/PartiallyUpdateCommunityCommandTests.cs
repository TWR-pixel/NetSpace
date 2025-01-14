using FluentAssertions;
using NetSpace.Community.Application.Community.Commands;
using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.Tests.Unit.Initializer;

namespace NetSpace.Community.Tests.Unit.Application.Community.Commands;

public sealed class PartiallyUpdateCommunityCommandTests
{
    [Fact]
    public async Task Should_ReturnUpdatedCommunityResponse()
    {
        #region Arrange
        var uof = await TestInitializer.CreateUnitOfWorkWithTestDataAsync();
        var command = new PartiallyUpdateCommunityCommand { Id = 2, Name = "newName" };
        var handler = new PartiallyUpdateCommunityCommandHandler(uof, TestMapper.Create(),new PartiallyUpdateCommunityCommandValidator());
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
        var command = new PartiallyUpdateCommunityCommand { Id = 22, Name = "newName" };
        var handler = new PartiallyUpdateCommunityCommandHandler(uof, TestMapper.Create(), new PartiallyUpdateCommunityCommandValidator());
        #endregion

        #region Act

        await Assert.ThrowsAsync<CommunityNotFoundException>(async () =>
        {
            var result = await handler.Handle(command, CancellationToken.None);
        });
        #endregion
    }
}
