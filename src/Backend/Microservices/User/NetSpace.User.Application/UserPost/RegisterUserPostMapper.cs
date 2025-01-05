﻿using Mapster;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.UserPost.Requests.Create;
using NetSpace.User.Application.UserPost.Requests.PartiallyUpdate;
using NetSpace.User.Application.UserPost.Requests.Update;
using NetSpace.User.Domain.UserPost;

namespace NetSpace.User.Application.UserPost;

public sealed class RegisterUserPostMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateUserPostRequest, UserPostEntity>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<UpdateUserPostRequest, UserPostEntity>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<PartiallyUpdateUserPostRequest, UserPostEntity>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<IEnumerable<UserPostEntity>, IEnumerable<UserPostResponse>>()
            .RequireDestinationMemberSource(true);

        #region Common messages

        config.NewConfig<UserPostEntity, UserUpdatedMessage>();
        config.NewConfig<UserPostEntity, UserDeletedMessage>();
        config.NewConfig<UserPostEntity, UserCreatedMessage>();

        #endregion
    }
}
