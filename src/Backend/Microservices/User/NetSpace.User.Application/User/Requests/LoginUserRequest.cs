using Microsoft.AspNetCore.Identity;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Domain.User;

namespace NetSpace.User.Application.User.Requests;

public sealed record LoginUserRequest : RequestBase<LoginUserResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public sealed record LoginUserResponse : ResponseBase;

public sealed class LoginUserRequestHandler(UserManager<UserEntity> userManager) : RequestHandlerBase<LoginUserRequest, LoginUserResponse>
{
    public override async Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        var userEntity = await userManager.FindByEmailAsync(request.Email)
            ?? throw new UserNotFoundException(request.Email);

        var result = await userManager.CheckPasswordAsync(userEntity, request.Password);

        if (!result)
            throw new NotRightPasswordException(request.Password);

        return new LoginUserResponse();
    }
}
