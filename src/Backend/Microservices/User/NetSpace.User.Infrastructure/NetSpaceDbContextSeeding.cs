using Microsoft.EntityFrameworkCore;
using NetSpace.User.Domain.User;

namespace NetSpace.User.Infrastructure;

public static class NetSpaceDbContextSeeding
{
    public static async void Seed(DbContext dbContext)
    {
        var user = new Domain.User.UserEntity
        {
            Id = Guid.NewGuid(),
            Nickname = "nick1",
            Name = "name1",
            Surname = "surname1",
            Email = "email@mail.ru"
        };

        dbContext.Set<UserEntity>().Add(user);

        var userPost = new UserPostEntity
        {
            Id = 1,
            Title = "title1",
            Body = "body1",
            UserId = user.Id
        };

        dbContext.Set<UserPostEntity>().Add(userPost);

        var userComment = new UserPostUserCommentEntity
        {
            Id = 1,
            Body = "body1",
            UserId = user.Id,
            Owner = user,
            UserPost = userPost,
            UserPostId = userPost.Id,
            CreatedAt = DateTime.UtcNow
        };

        dbContext.Set<UserPostUserCommentEntity>().Add(userComment);
        dbContext.SaveChanges();

    }
}
