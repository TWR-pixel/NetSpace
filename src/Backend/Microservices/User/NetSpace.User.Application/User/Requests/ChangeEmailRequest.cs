using Microsoft.AspNetCore.Identity;
using NetSpace.Common.Application;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Domain;

namespace NetSpace.User.Application.User.Requests;

public sealed record ChangeEmailRequest : RequestBase<ChangeUserEmailResponse>
{
    public required string NewEmail { get; set; }
}

public sealed record ChangeUserEmailResponse : ResponseBase;

public sealed class ChangeUserEmailRequestHandler(UserManager<UserEntity> userManager) : RequestHandlerBase<ChangeEmailRequest, ChangeUserEmailResponse>
{
    public override async Task<ChangeUserEmailResponse> Handle(ChangeEmailRequest request, CancellationToken cancellationToken)
    {
        var userEntity = await userManager.FindByEmailAsync(request.NewEmail) ?? throw new UserNotFoundException(request.NewEmail);

        var token = await userManager.GenerateChangeEmailTokenAsync(userEntity, request.NewEmail);
        await userManager.ChangeEmailAsync(userEntity, request.NewEmail, token);

        return new ChangeUserEmailResponse();
    }
}