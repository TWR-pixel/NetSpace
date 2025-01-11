
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using NetSpace.Identity.Application.User.Exceptions;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Application.User.Commands.Email;

public sealed record ConfirmEmailCommand : RequestBase<UserResponse>
{
    public required string Email { get; set; }
    public required string Token { get; set; }
}

public sealed class ConfirmEmailCommandHandler(UserManager<UserEntity> userManager, IMapper mapper) : RequestHandlerBase<ConfirmEmailCommand, UserResponse>
{
    public override async Task<UserResponse> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await userManager.FindByEmailAsync(request.Email)
            ?? throw new UserNotFoundException(request.Email);

        await userManager.ConfirmEmailAsync(userEntity, request.Token);

        return mapper.Map<UserResponse>(userEntity);
    }
}

