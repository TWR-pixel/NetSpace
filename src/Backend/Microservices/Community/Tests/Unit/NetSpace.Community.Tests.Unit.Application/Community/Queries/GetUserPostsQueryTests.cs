using FluentAssertions;
using MassTransit.Testing;
using NetSpace.Community.Application.Community.Queries;
using NetSpace.Community.Tests.Unit.Initializer;

namespace NetSpace.Community.Tests.Unit.Application.Community.Queries;

public sealed class GetUserPostsQueryTests
{
    [Fact]
    public async Task Should_ReturnCommunityWithFilterByTitle()
    {
        #region Arrange
        var uof = await TestInitializer.CreateReadonlyUnitOfWorkWithUserAndCommunitiesTestDataAsync();
        var query = new GetCommunityQuery { Filter = new UseCases.Community.CommunityFilterOptions { Id = 2 } };
        var handler = new GetCommunityQueryHandler(uof, TestMapper.Create());
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
        var uof = await TestInitializer.CreateReadonlyUnitOfWorkWithUserAndCommunitiesTestDataAsync();
        var query = new GetCommunityQuery { Filter = new UseCases.Community.CommunityFilterOptions { Id = 322 } };
        var handler = new GetCommunityQueryHandler(uof, TestMapper.Create());
        #endregion

        #region Act
        var result = await handler.Handle(query, CancellationToken.None);
        #endregion

        result.Should().BeEmpty();
        result.Should().NotBeNull();
    }
}
