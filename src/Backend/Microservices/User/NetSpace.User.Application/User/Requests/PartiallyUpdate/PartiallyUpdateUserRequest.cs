
namespace NetSpace.User.Application.User.Requests.PartiallyUpdate;

public sealed record PartiallyUpdateUserRequest : RequestBase<UserResponse>
{
}

public sealed class PartiallyUpdateUserRequestHandler : RequestHandlerBase<PartiallyUpdateUserRequest, UserResponse>
{
    public override Task<UserResponse> Handle(PartiallyUpdateUserRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
