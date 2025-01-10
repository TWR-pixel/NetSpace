﻿using Mapster;
using NetSpace.Common.Messages.User;
using NetSpace.Identity.Application.User.Queries;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Application.User;

public sealed class RegisterUserMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<JwtUserRegistrationRequest, UserEntity>();

        #region Common messages

        config.NewConfig<UserUpdatedMessage, UserEntity>();
        config.NewConfig<UserDeletedMessage, UserEntity>();

        config.NewConfig<UserEntity, UserCreatedMessage>();
        #endregion
    }
}
