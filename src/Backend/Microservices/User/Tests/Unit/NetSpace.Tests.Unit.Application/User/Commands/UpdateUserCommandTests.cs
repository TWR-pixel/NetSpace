using FluentAssertions;
using MassTransit;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NetSpace.Common.Messages.User;
using NetSpace.Tests.Unit.Initializer;
using NetSpace.User.Application.User;
using NetSpace.User.Application.User.Commands;
using NetSpace.User.Application.User.Exceptions;

namespace NetSpace.Tests.Unit.Application.User.Commands;

public sealed class UpdateUserCommandTests
{
    [Fact]
    public async Task Should_ReturnUpdatedUserResponse()
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
