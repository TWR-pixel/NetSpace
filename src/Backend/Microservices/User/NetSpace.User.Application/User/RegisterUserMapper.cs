using Mapster;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.User.Commands.Delete;
using NetSpace.User.Application.User.Commands.PartiallyUpdate;
using NetSpace.User.Application.User.Commands.Update;
using NetSpace.User.Domain.User;

namespace NetSpace.User.Application.User;

public sealed class RegisterUserMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<IEnumerable<UserEntity>, IEnumerable<UserResponse>>();
        config.NewConfig<UpdateUserCommand, UserEntity>();
        config.NewConfig<PartiallyUpdateUserCommand, UserEntity>()
            .IgnoreNullValues(true)
            .Ignore(u => u.Id);

        config.NewConfig<DeleteUserByIdCommand, UserEntity>();
        
        #region Common messages

        config.NewConfig<UserEntity, UserCreatedMessage>();
        config.NewConfig<UserEntity, UserUpdatedMessage>();
        config.NewConfig<UserEntity, UserDeletedMessage>();

        #endregion
    }
}
