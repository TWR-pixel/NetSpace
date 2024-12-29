using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Caching.Distributed;
using NetSpace.Common.Application;
using NetSpace.User.Domain;
using System.Text.Json;

namespace NetSpace.User.Application.User.Requests;

public sealed record GetUserByIdRequest : RequestBase<GetUserByIdResponse>
{
    public Guid Id { get; set; }
}

public sealed record GetUserByIdResponse : ResponseBase;

public sealed class GetUserByIdRequestHandler(UserManager<UserEntity> userManager, IDistributedCache distributedCache) : RequestHandlerBase<GetUserByIdRequest, GetUserByIdResponse>
{
    public override async Task<GetUserByIdResponse> Handle(GetUserByIdRequest request, CancellationToken cancellationToken)
    {
        UserEntity? userEntity = default;
        var cachedUser = await distributedCache.GetStringAsync(request.Id.ToString(), cancellationToken);

        if (cachedUser == null)
        {
            userEntity = await userManager.FindByIdAsync(request.Id.ToString());

            await distributedCache.SetStringAsync(request.Id.ToString(), JsonSerializer.Serialize(userEntity), cancellationToken);

            return new GetUserByIdResponse();
        }

        return new GetUserByIdResponse();
    }
}
