using FluentAssertions;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Moq;
using NetSpace.Common.Messages.User;
using NetSpace.Tests.Unit.Initializer;
using NetSpace.User.Application.User;
using NetSpace.User.Application.User.Commands.Update;
using NetSpace.User.Application.User.Exceptions;

namespace NetSpace.Tests.Unit.Application.User.Commands.Update;

public sealed class UpdateUserCommandTests
{
    [Fact]
    public async Task Should_ReturnDeletedUserResponse()
    {
        #region Arrange
        var mockEndpoint = new Mock<IPublishEndpoint>();
        mockEndpoint
            .Setup(m => m.Publish<UserDeletedMessage>(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var provider = new ServiceCollection()
                .AddMassTransitTestHarness(configure =>
                {
                    configure.UsingInMemory((bus, conf) =>
                    {
                        conf.Publish<UserUpdatedMessage>();
                    });
                })
                .BuildServiceProvider(true);

        var harness = provider.GetTestHarness();
        await harness.Start();

        var testUsers = TestInitializer.Create3Users();
        var uof = await TestInitializer.CreateUnitOfWorkAsync();
        await uof.Users.AddRangeAsync(testUsers);
        await uof.SaveChangesAsync();
        var command = new UpdateUserCommand { Id = testUsers.First().Id, Name = "newName", Nickname = "newNickname", Surname = "newSurname" };
        var memCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        var testCache = new FakeUserDistributedCacheStorage(memCache);
        var handler = new UpdateUserCommandHandler(uof, new UpdateUserCommandValidator(), TestMapper.Create(), mockEndpoint.Object);
        #endregion

        #region Act
        var result = await handler.Handle(command, CancellationToken.None);
        #endregion

        result.Should().NotBeNull();
        result.Should().Be(TestMapper.Create().Map<UserResponse>(testUsers.First()));
    }

    [Fact]
    public async Task Should_ThrowUserNotFoundException()
    {
        #region Arrange
        var mockEndpoint = new Mock<IPublishEndpoint>();
        mockEndpoint
            .Setup(m => m.Publish<UserDeletedMessage>(It.IsAny<object>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        var provider = new ServiceCollection()
                .AddMassTransitTestHarness(configure =>
                {
                    configure.UsingInMemory((bus, conf) =>
                    {
                        conf.Publish<UserUpdatedMessage>();
                    });
                })
                .BuildServiceProvider(true);

        var harness = provider.GetTestHarness();
        await harness.Start();

        var testUsers = TestInitializer.Create3Users();
        var uof = await TestInitializer.CreateUnitOfWorkAsync();
        await uof.Users.AddRangeAsync(testUsers);
        await uof.SaveChangesAsync();
        var command = new UpdateUserCommand { Id = Guid.NewGuid(), Name = "newName", Nickname = "newNickname", Surname = "newSurname" };
        var memCache = new MemoryCache(Options.Create(new MemoryCacheOptions()));
        var testCache = new FakeUserDistributedCacheStorage(memCache);
        var handler = new UpdateUserCommandHandler(uof, new UpdateUserCommandValidator(), TestMapper.Create(), mockEndpoint.Object);
        #endregion

        #region Act

        await Assert.ThrowsAsync<UserNotFoundException>(async () =>
        {
            var result = await handler.Handle(command, CancellationToken.None);
        });
        #endregion
    }
}
