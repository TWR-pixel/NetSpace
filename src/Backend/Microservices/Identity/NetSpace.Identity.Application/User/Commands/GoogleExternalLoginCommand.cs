using System.Security.Claims;

namespace NetSpace.Identity.Application.User.Commands;

public sealed record GoogleExternalLoginCommand : RequestBase<UserResponse>
{
    public required ClaimsPrincipal User { get; set; }
}

public sealed class GoogleExternalLoginCommandHandler : RequestHandlerBase<GoogleExternalLoginCommand, UserResponse>
{
    public override Task<UserResponse> Handle(GoogleExternalLoginCommand request, CancellationToken cancellationToken)
    {
        var user = request.User;

        if (user?.Identity != null && user.Identity.IsAuthenticated)
        {
            return this.Ok(new UserModel
            {
                IsAuthenticated = true,
                Name = user.FindFirst("name")?.Value,
                EmailAddress = user.FindFirst(c => c.Type == ClaimTypes.Email)?.Value,
                Phone = user.FindFirst(c => c.Type == "phone")?.Value
            });
        }
    }
}
