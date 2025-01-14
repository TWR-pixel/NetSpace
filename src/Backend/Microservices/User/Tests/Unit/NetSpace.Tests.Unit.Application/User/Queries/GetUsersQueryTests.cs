using FluentAssertions;
using MassTransit.Testing;
using NetSpace.User.Application.User.Queries;
using NetSpace.User.Tests.Unit.Initializer;

namespace NetSpace.User.Tests.Unit.Application.User.Queries;

public sealed class GetUsersQueryTests
{
    [Fact]
    public async Task Should_ReturnUserWithFilterByName()
    {
        #region Arrange
        var uof = await TestInitializer.CreateReadonlyUnitOfWorkWithUserAndUserPostsAndUserPostUserCommentsAsync();
        var query = new GetUsersQuery { Filter = new UseCases.User.UserFilterOptions { Nickname = "nickname2" } };
        var handler = new GetUsersQueryHandler(uof, TestMapper.Create());
        #endregion

        #region Act
        var result = await handler.Handle(query, CancellationToken.None);
        var resultCount = result.Count();
        #endregion

        resultCount.Should().Be(1);
        result.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Should_ReturnEmptyAndNullEnumerable()
    {
        #region Arrange
        var uof = await TestInitializer.CreateReadonlyUnitOfWorkWithUserAndUserPostsAndUserPostUserCommentsAsync();
        var query = new GetUsersQuery { Filter = new UseCases.User.UserFilterOptions { Id = Guid.NewGuid() } };
        var handler = new GetUsersQueryHandler(uof, TestMapper.Create());
        #endregion

        #region Act
        var result = await handler.Handle(query, CancellationToken.None);
        #endregion

        result.Should().BeEmpty();
        result.Should().NotBeNull();
    }
}
