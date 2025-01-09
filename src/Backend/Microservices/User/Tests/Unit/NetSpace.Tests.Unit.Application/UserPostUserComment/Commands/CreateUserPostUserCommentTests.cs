using NetSpace.Tests.Unit.Initializer;
using FluentAssertions;
using NetSpace.User.Application.UserPostUserComment;
using NetSpace.User.Application.UserPostUserComment.Commands;

namespace NetSpace.Tests.Unit.Application.UserPostUserComment.Commands;

public sealed class CreateUserPostUserCommentTests
{
    [Fact]
    public async Task Should_CreateUserPostUserCommentWithValidParameters()
    {
        #region Arrange

        var testUsers = TestInitializer.Create3Users();
        var testPosts = TestInitializer.Create3UserPosts(testUsers);
        var uof = await TestInitializer.CreateUnitOfWorkAsync();
        await uof.Users.AddRangeAsync(testUsers);
        await uof.UserPosts.AddRangeAsync(testPosts);
        await uof.UserPostUserComments.AddAsync(new NetSpace.User.Domain.UserPostUserComment
            .UserPostUserCommentEntity
        { Body = "testBody", Owner = testUsers[0], UserId = testUsers[0].Id, UserPost = testPosts[0], UserPostId = testPosts[0].Id });
        await uof.SaveChangesAsync();

        var command = new CreateUserPostUserCommentCommand { Body = "testBody", UserId = testUsers.First().Id, UserPostId = 1 };
        var handler = new CreateUserPostUserCommentCommandHandler(uof, TestMapper.Create(), new CreateUserPostUserCommentCommandValidator());
        #endregion

        #region Act
        var result = await handler.Handle(command, CancellationToken.None);
        #endregion

        #region Assert
        result.Should().NotBeNull();
        result.Should().Be(TestMapper.Create().Map<UserPostUserCommentResponse>(await uof.UserPostUserComments.FindByIdAsync(result.Id)));
        #endregion
    }
}
