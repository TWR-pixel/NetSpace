using NetSpace.Common.Application;

namespace NetSpace.User.Application.User.Requests;

public sealed record LoginUserRequest : RequestBase<LoginUserResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public sealed record LoginUserResponse : ResponseBase;

public sealed class LoginUserRequestHandler : RequestHandlerBase<LoginUserRequest, LoginUserResponse>
{
    public override Task<LoginUserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
