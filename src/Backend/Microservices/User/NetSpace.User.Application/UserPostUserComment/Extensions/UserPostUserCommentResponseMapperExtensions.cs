using NetSpace.User.Application.User.Extensions;
using NetSpace.User.Application.UserPost.Extensions;
using NetSpace.User.Domain.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment.Extensions;

public static class UserPostUserCommentResponseMapperExtensions
{
    public static UserPostUserCommentResponse ToResponse(this UserPostUserCommentEntity entity)
    {
        var response = new UserPostUserCommentResponse
        {
            Id = entity.Id,
            Body = entity.Body,
            CreatedAt = entity.CreatedAt,
            Owner = entity.Owner?.ToResponse(),
            UserId = entity.UserId,
            UserPost = entity.UserPost?.ToResponse(),
            UserPostId = entity.UserPostId,
        };

        return response;
    }

    public static IEnumerable<UserPostUserCommentResponse> ToResponses(this IEnumerable<UserPostUserCommentEntity> entities)
    {
        var responses = entities.Select(e => e.ToResponse());

        return responses;
    }
}
