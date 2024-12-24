using NetSpace.Common.Application;

namespace NetSpace.User.Application.User.Requests;

public sealed record CreateUserRequest : RequestBase<CreateUserResponse>
{
}

public sealed record CreateUserResponse : ResponseBase;

public sealed class CreateUserRequestHandler : RequestHandlerBase<CreateUserRequest, CreateUserResponse>
{
    public async override Task<CreateUserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        return new CreateUserResponse();
    }
}