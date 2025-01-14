using FluentAssertions;
using MassTransit.Testing;
using NetSpace.User.Application.UserPostUserComment.Queries;
using NetSpace.User.Tests.Unit.Initializer;

namespace NetSpace.User.Tests.Unit.Application.UserPostUserComment.Queries;

public sealed class GetUserPostUserCommentsQueryTests
{
    [Fact]
    public async Task Should_ReturnUserPostWithFilterByTitle()
    {
        #region Arrange
        var uof = await TestInitializer.CreateReadonlyUnitOfWorkWithUserAndUserPostsAndUserPostUserCommentsAsync();
        var query = new GetUserPostUserCommentQuery
        {
            Filter = new UseCases.UserPostUserComment.UserPostUserCommentFilterOptions
            { Body = "TestBody1" }
        };
        var handler = new GetUserPostUserCommentRequestHandler(uof, TestMapper.Create());
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
        var query = new GetUserPostUserCommentQuery
        {
            Filter = new UseCases.UserPostUserComment.UserPostUserCommentFilterOptions
            { Body = "TestBod231y1" }
        };
        var handler = new GetUserPostUserCommentRequestHandler(uof, TestMapper.Create());
        #endregion

        #region Act
        var result = await handler.Handle(query, CancellationToken.None);
        #endregion

        result.Should().BeEmpty();
        result.Should().NotBeNull();
    }
}
