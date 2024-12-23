using NetSpace.Common.Application;

namespace NetSpace.User.Application.Requests;

public sealed record RegisterUserRequest : RequestBase<RegisterUserResponse>
{
}

public sealed record RegisterUserResponse : ResponseBase;

public sealed class RegisterUserRequestHandler : RequestHandlerBase<RegisterUserRequest, RegisterUserResponse>
{
    public override Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
