using NetSpace.User.Domain.User;
using NetSpace.User.Domain.UserPostUserComment;

namespace NetSpace.User.Infrastructure;

public static class NetSpaceDbContextSeeding
{
    public static async void Seed(NetSpaceDbContext dbContext)
    {
        var user = new UserEntity
        {
            Id = Guid.NewGuid(),
            Nickname = "nick1",
            Name = "name1",
            Surname = "surname1",
            Email = "email@mail.ru"
        };
        var user2 = new UserEntity
        {
            Id = Guid.NewGuid(),
            Nickname = "nick2",
            Name = "name2",
            Surname = "surname2",
            Email = "email2@mail.ru"
        };
        var user3 = new UserEntity
        {
            Id = Guid.NewGuid(),
            Nickname = "nick3",
            Name = "name3",
            Surname = "surname3",
            Email = "email3@mail.ru"
        };

        await dbContext.Set<UserEntity>().AddRangeAsync(user, user2, user3);

        var userPost = new UserPostEntity
        {
            Id = 1,
            Title = "title1",
            Body = "body1",
            UserId = user.Id,
            User = user,
        };
        var userPost2 = new UserPostEntity
        {
            Id = 2,
            Title = "title2",
            Body = "body2",
            UserId = user.Id,
            User = user
        };
        var userPost3 = new UserPostEntity
        {
            Id = 3,
            Title = "title3",
            Body = "body3",
            UserId = user.Id,
            User = user,
        };

        await dbContext.Set<UserPostEntity>().AddRangeAsync(userPost, userPost2, userPost3);

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
        var userComment2 = new UserPostUserCommentEntity
        {
            Id = 2,
            Body = "body2eifjweoijfw",
            UserId = user.Id,
            Owner = user,
            UserPost = userPost2,
            UserPostId = userPost2.Id,
        };

        await dbContext.Set<UserPostUserCommentEntity>().AddAsync(userComment);
        await dbContext.Set<UserPostUserCommentEntity>().AddAsync(userComment2);
        await dbContext.SaveChangesAsync();

    }
}
