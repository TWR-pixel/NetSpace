using NetSpace.User.Application.User.Extensions;
using NetSpace.User.Application.UserPostUserComment.Extensions;
using NetSpace.User.Domain.User;

namespace NetSpace.User.Application.UserPost.Extensions;

public static class UserPostResponseMapperExtensions
{
    public static UserPostResponse ToResponse(this UserPostEntity entity)
    {
        var response = new UserPostResponse
        {
            Id = entity.Id,
            Title = entity.Title,
            Body = entity.Body,
            User = entity.User?.ToResponse(),
            UserId = entity.UserId,
            UserComments = entity.UserComments.ToResponses()
        };

        return response;
    }

    public static IEnumerable<UserPostResponse> ToResponses(this IEnumerable<UserPostEntity> entities)
    {
        var responses = entities.Select(e => e.ToResponse());

        return responses;
    }

}
