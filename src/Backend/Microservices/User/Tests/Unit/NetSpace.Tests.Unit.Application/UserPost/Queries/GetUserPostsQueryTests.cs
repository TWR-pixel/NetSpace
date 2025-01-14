using FluentAssertions;
using MassTransit.Testing;
using NetSpace.User.Application.UserPost.Queries;
using NetSpace.User.Tests.Unit.Initializer;

namespace NetSpace.User.Tests.Unit.Application.UserPost.Queries;

public sealed class GetUserPostsQueryTests
{
    [Fact]
    public async Task Should_ReturnUserPostWithFilterByTitle()
    {
        #region Arrange
        var uof = await TestInitializer.CreateReadonlyUnitOfWorkWithUserAndUserPostsAndUserPostUserCommentsAsync();
        var query = new GetUserPostQuery { Filter = new UseCases.UserPost.UserPostFilterOptions { Title = "TestTitle1" } };
        var handler = new GetUserPostQueryHandler(uof, TestMapper.Create());
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
        var query = new GetUserPostQuery { Filter = new UseCases.UserPost.UserPostFilterOptions { Title = "TestTitle512" } };
        var handler = new GetUserPostQueryHandler(uof, TestMapper.Create());
        #endregion

        #region Act
        var result = await handler.Handle(query, CancellationToken.None);
        #endregion

        result.Should().BeEmpty();
        result.Should().NotBeNull();
    }
}
