using Microsoft.AspNetCore.Identity;
using NetSpace.Common.Application;
using NetSpace.User.Domain;

namespace NetSpace.User.Application.User.Requests;

public sealed record RegisterUserRequest : RequestBase<RegisterUserResponse>
{
}

public sealed record RegisterUserResponse : ResponseBase
{

}

public sealed class RegisterUserRequestHandler(UserManager<UserEntity> userManager) : RequestHandlerBase<RegisterUserRequest, RegisterUserResponse>
{
    public override Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
