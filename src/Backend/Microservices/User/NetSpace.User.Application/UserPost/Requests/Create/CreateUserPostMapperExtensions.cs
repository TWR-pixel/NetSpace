using NetSpace.User.Domain.UserPost;

namespace NetSpace.User.Application.UserPost.Requests.Create;

public static class CreateUserPostMapperExtensions
{
    public static UserPostEntity ToEntity(this CreateUserPostRequest request)
    {
        var entity = new UserPostEntity
        {
            Body = request.Body,
            Title = request.Title,
            UserId = request.OwnerId,
        };

        return entity;
    }
}
