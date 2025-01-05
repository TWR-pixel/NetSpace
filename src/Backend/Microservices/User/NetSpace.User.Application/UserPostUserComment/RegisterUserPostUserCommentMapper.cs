using Mapster;
using NetSpace.User.Application.UserPostUserComment.Commands.Create;
using NetSpace.User.Application.UserPostUserComment.Commands.PartiallyUpdate;
using NetSpace.User.Application.UserPostUserComment.Commands.Update;
using NetSpace.User.Domain.UserPost;
using NetSpace.User.Domain.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment;

public sealed class RegisterUserPostUserCommentMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserPostUserCommentCommand, UserPostUserCommentEntity>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<UpdateUserPostUserCommentCommand, UserPostUserCommentEntity>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<IEnumerable<UserPostEntity>, IEnumerable<UserPostUserCommentResponse>>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<PartiallyUpdateUserPostUserCommentCommand, UserPostUserCommentEntity>()
            .RequireDestinationMemberSource(true);

        #region Common messages


        #endregion
    }
}
