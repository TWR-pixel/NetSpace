using Mapster;
using NetSpace.User.Application.UserPostUserComment.Commands;
using NetSpace.User.Domain.UserPost;
using NetSpace.User.Domain.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment;

public sealed class RegisterUserPostUserCommentMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserPostUserCommentCommand, UserPostUserCommentEntity>();
        config.NewConfig<UpdateUserPostUserCommentCommand, UserPostUserCommentEntity>();
        config.NewConfig<IEnumerable<UserPostEntity>, IEnumerable<UserPostUserCommentResponse>>();
        config.NewConfig<PartiallyUpdateUserPostUserCommentCommand, UserPostUserCommentEntity>();
    }
}
