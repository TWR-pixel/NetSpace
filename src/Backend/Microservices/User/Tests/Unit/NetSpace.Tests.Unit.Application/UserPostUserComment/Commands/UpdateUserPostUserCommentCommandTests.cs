﻿using FluentAssertions;
using NetSpace.User.Application.UserPostUserComment.Commands;
using NetSpace.User.Application.UserPostUserComment.Exceptions;
using NetSpace.User.Tests.Unit.Initializer;

namespace NetSpace.User.Tests.Unit.Application.UserPostUserComment.Commands;

public sealed class UpdateUserPostUserCommentCommandTests
{
    [Fact]
    public async Task Should_ReturnUpdatedUserPostResponse()
    {
        #region Arrange
        var testUsers = TestInitializer.Create3Users();
        var testPosts = TestInitializer.Create3UserPosts(testUsers);
        var uof = await TestInitializer.CreateUnitOfWorkAsync();
        await uof.Users.AddRangeAsync(testUsers);
        await uof.UserPosts.AddRangeAsync(testPosts);
        await uof.UserPostUserComments.AddAsync(new Domain.UserPostUserComment
            .UserPostUserCommentEntity
        { Body = "testBody", Owner = testUsers[0], UserId = testUsers[0].Id, UserPost = testPosts[0], UserPostId = testPosts[0].Id });
        await uof.SaveChangesAsync();

        var command = new UpdateUserPostUserCommentCommand { Id = 1, Body = "newBody" };
        var handler = new UpdateUserPostUserCommentCommandHandler(uof, TestMapper.Create(), new UpdateUserPostUserCommentCommandValidator());
        #endregion

        #region Act
        var result = await handler.Handle(command, CancellationToken.None);
        #endregion

        result.Should().NotBeNull();
    }

    [Fact]
    public async Task Should_ThrowUserPostNotFoundException()
    {
        #region Arrange
        var testUsers = TestInitializer.Create3Users();
        var testPosts = TestInitializer.Create3UserPosts(testUsers);
        var uof = await TestInitializer.CreateUnitOfWorkAsync();
        await uof.Users.AddRangeAsync(testUsers);
        await uof.UserPosts.AddRangeAsync(testPosts);
        await uof.UserPostUserComments.AddAsync(new Domain.UserPostUserComment
            .UserPostUserCommentEntity
        { Body = "testBody", Owner = testUsers[0], UserId = testUsers[0].Id, UserPost = testPosts[0], UserPostId = testPosts[0].Id });
        await uof.SaveChangesAsync();

        var command = new UpdateUserPostUserCommentCommand { Id = 23, Body = "newBody" };
        var handler = new UpdateUserPostUserCommentCommandHandler(uof, TestMapper.Create(), new UpdateUserPostUserCommentCommandValidator());
        #endregion

        #region Act

        await Assert.ThrowsAsync<UserPostUserCommentNotFoundException>(async () =>
        {
            var result = await handler.Handle(command, CancellationToken.None);
        });
        #endregion
    }
}
