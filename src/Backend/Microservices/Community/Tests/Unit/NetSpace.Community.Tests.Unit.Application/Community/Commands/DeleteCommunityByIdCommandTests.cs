using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using NetSpace.Community.Application.Community.Commands;
using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.Tests.Unit.Initializer;

namespace NetSpace.Community.Tests.Unit.Application.Community.Commands;

public sealed class DeleteCommunityByIdCommandTests
{
    [Fact]
    public async Task Should_ReturnDeletedCommunityResponse()
    {
        #region Arrange

        var uof = await TestInitializer.CreateUnitOfWorkWithTestDataAsync();
        var users = await uof.Users.GetAllAsync();
        var memCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        var communityCache = new CommunityDistributedCache(memCache);
        var command = new DeleteCommunityCommand { Id = 2 };
        var handler = new DeleteCommunityCommandHandler(uof, TestMapper.Create(), communityCache);
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
        var memCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        var communityCache = new CommunityDistributedCache(memCache);
        var command = new DeleteCommunityCommand { Id = 32 };
        var handler = new DeleteCommunityCommandHandler(uof, TestMapper.Create(), communityCache);
        #endregion

        #region Act

        await Assert.ThrowsAsync<CommunityNotFoundException>(async () =>
        {
            var result = await handler.Handle(command, CancellationToken.None);
        });
        #endregion
    }
}
