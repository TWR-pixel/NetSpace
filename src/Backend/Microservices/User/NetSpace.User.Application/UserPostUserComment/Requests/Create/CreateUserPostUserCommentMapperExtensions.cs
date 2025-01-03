using NetSpace.User.Domain.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment.Requests.Create;

public static class CreateUserPostUserCommentMapperExtensions
{
    public static UserPostUserCommentEntity ToEntity(this CreateUserPostUserCommentRequest request)
    {
        var entity = new UserPostUserCommentEntity
        {
            Body = request.Body,
            UserId = request.UserId,
            UserPostId = request.UserPostId,
            CreatedAt = request.CreatedAt,
        };

        return entity;
    }
}
