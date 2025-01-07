using NetSpace.Identity.UseCases.User;

namespace NetSpace.Identity.Application.User.Requests;

public sealed record LoginUserRequest : RequestBase<UserResponse>
{
    public required string Email { get; set; }
}

public sealed class LoginUserRequestHandler(IUserRepository userRepository) : RequestHandlerBase<LoginUserRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(LoginUserRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();

        //var userFromDb = await userRepository.FindByEmailAsync(request.Email);
        //throw new NotImplementedException();
        //var response = new UserResponse { }

        //return response;
    }
}
