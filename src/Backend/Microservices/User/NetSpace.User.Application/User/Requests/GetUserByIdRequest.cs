using Microsoft.AspNetCore.Identity;
using NetSpace.User.Application.Common.Cache;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Domain.User;

namespace NetSpace.User.Application.User.Requests;

public sealed record GetUserByIdRequest : RequestBase<GetUserByIdResponse>
{
    public Guid Id { get; set; }
}

public sealed record GetUserByIdResponse : ResponseBase;

public sealed class GetUserByIdRequestHandler(UserManager<UserEntity> userManager, IUserDistributedCacheStorage cache) : RequestHandlerBase<GetUserByIdRequest, GetUserByIdResponse>
{
    public override async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        var cachedUser = await cache.GetByIdAsync(request.Id.ToString(), cancellationToken);

        if (cachedUser == null)
        {
            var userEntity = await userManager.FindByIdAsync(request.Id.ToString())
                ?? throw new UserNotFoundException(request.Id);

            await cache.AddAsync(userEntity, cancellationToken);

            return new GetUserByIdResponse();
        }

        return new GetUserByIdResponse();
    }
}
