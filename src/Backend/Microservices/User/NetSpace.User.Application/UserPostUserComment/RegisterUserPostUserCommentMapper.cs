using Mapster;
using NetSpace.User.Application.UserPostUserComment.Requests.Create;
using NetSpace.User.Application.UserPostUserComment.Requests.PartiallyUpdate;
using NetSpace.User.Application.UserPostUserComment.Requests.Update;
using NetSpace.User.Domain.UserPost;
using NetSpace.User.Domain.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment;

public sealed class RegisterUserPostUserCommentMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserPostUserCommentRequest, UserPostUserCommentEntity>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<UpdateUserPostUserCommentRequest, UserPostUserCommentEntity>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<IEnumerable<UserPostEntity>, IEnumerable<UserPostUserCommentResponse>>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<PartiallyUpdateUserPostUserCommentRequest, UserPostUserCommentEntity>()
            .RequireDestinationMemberSource(true);
    }
}
