using NetSpace.User.Domain.User;

namespace NetSpace.User.Application.UserPost.Extensions;

public static class UserPostRequestMapperExtensions
{
    public static UserPostEntity ToEntity(this UserPostRequest request, UserEntity userEntity)
    {
        var entity = new UserPostEntity()
        {
            Title = request.Title,
            Body = request.Body,
            UserId = request.OwnerId,
            User = userEntity
        };

        return entity;
    }
}
