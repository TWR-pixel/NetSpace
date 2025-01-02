using NetSpace.User.Domain.User;
using NetSpace.User.Domain.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment.Extensions;

public static class UserPostUserCommentRequestMapperExtensions
{
    public static UserPostUserCommentEntity ToEntity(this UserPostUserCommentRequest request, UserEntity owner, UserPostEntity userPost)
    {
        var entity = new UserPostUserCommentEntity
        {
            Body = request.Body,
            Owner = owner,
            UserId = owner.Id,
            UserPost = userPost,
            UserPostId = userPost.Id,
        };

        return entity;
    }


}
