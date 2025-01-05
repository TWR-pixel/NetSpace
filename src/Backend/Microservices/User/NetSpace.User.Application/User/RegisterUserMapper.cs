using Mapster;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.User.Requests.Create;
using NetSpace.User.Application.User.Requests.Delete;
using NetSpace.User.Application.User.Requests.PartiallyUpdate;
using NetSpace.User.Application.User.Requests.Update;
using NetSpace.User.Domain.User;

namespace NetSpace.User.Application.User;

public sealed class RegisterUserMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserRequest, UserEntity>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<IEnumerable<UserEntity>, IEnumerable<UserResponse>>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<UpdateUserRequest, UserEntity>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<PartiallyUpdateUserRequest, UserEntity>()
            .IgnoreNullValues(true)
            .Ignore(u => u.Id);

        config.NewConfig<DeleteUserByIdRequest, UserEntity>()
            .RequireDestinationMemberSource(true);

        #region Common messages

        config.NewConfig<UserEntity, UserCreatedMessage>();
        config.NewConfig<UserEntity, UserUpdatedMessage>();
        config.NewConfig<UserEntity, UserDeletedMessage>();

        #endregion
    }
}
