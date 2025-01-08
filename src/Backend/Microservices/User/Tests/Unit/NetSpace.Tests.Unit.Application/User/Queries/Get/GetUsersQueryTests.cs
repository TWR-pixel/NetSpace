using FluentAssertions;
using MassTransit.Testing;
using NetSpace.Tests.Unit.Initializer;
using NetSpace.User.Application.User.Queries.Get;

namespace NetSpace.Tests.Unit.Application.User.Queries.Get;

public sealed class GetUsersQueryTests
{
    [Fact]
    public async Task Should_ReturnUserWithFilterByName()
    {
        #region Arrange
        var uof = await TestInitializer.CreateReadonlyUnitOfWorkWithUserAsync();
        var query = new GetUsersQuery { Filter = new NetSpace.User.UseCases.User.UserFilterOptions { Nickname = "nickname1" } };
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
        var uof = await TestInitializer.CreateReadonlyUnitOfWorkAsync();
        var query = new GetUsersQuery { };
        var handler = new GetUsersQueryHandler(uof, TestMapper.Create());
        #endregion

        #region Act
        var result = await handler.Handle(query, CancellationToken.None);
        #endregion

        result.Should().BeEmpty();
        result.Should().NotBeNull();
    }
}
