using Mapster;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.UserPost.Commands;
using NetSpace.User.Domain.UserPost;

namespace NetSpace.User.Application.UserPost;

public sealed class RegisterUserPostMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserPostCommand, UserPostEntity>();
        config.NewConfig<UpdateUserPostCommand, UserPostEntity>();
        config.NewConfig<PartiallyUpdateUserPostCommand, UserPostEntity>();
        config.NewConfig<IEnumerable<UserPostEntity>, IEnumerable<UserPostResponse>>();
    }
}
