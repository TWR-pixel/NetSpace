using Mapster;
using NetSpace.Common.Messages.User;
using NetSpace.Identity.Application.User.Requests;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Application.User;

public sealed class RegisterUserMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<RegisterUserRequest, UserEntity>();

        #region Common messages

        config.NewConfig<RegisterUserRequest, UserCreatedMessage>();
        config.NewConfig<UserCreatedMessage, UserEntity>();
        config.NewConfig<UserUpdatedMessage, UserEntity>();
        config.NewConfig<UserDeletedMessage, UserEntity>();

        #endregion
    }
}
