
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Application.User.Requests;

public sealed record GetUsersByFilterRequest : RequestBase<IEnumerable<UserResponse>>
{
}

public sealed class GetUsersByFilterRequestHandler(IUserRepository userRepository) : RequestHandlerBase<GetUsersByFilterRequest, IEnumerable<UserResponse>>
{
    public override Task<IEnumerable<UserResponse>> Handle(GetUsersByFilterRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
