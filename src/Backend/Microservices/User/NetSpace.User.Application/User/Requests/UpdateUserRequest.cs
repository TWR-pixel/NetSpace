using NetSpace.Common.Application;

namespace NetSpace.User.Application.User.Requests;

public sealed record UpdateUserRequest : RequestBase<UpdateUserResponse>
{
}

public sealed record UpdateUserResponse : ResponseBase;

public sealed class UpdateUserRequestHandler : RequestHandlerBase<UpdateUserRequest, UpdateUserResponse>
{
    public override Task<UpdateUserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
