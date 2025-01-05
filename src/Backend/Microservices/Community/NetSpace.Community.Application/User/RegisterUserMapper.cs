using Mapster;
using NetSpace.Common.Messages.User;
using NetSpace.Community.Domain.User;

namespace NetSpace.Community.Application.User;

public sealed class RegisterUserMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<UserCreatedMessage, UserEntity>();
    }
}
