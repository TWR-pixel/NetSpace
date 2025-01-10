
using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using NetSpace.Identity.Application.User.Exceptions;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Application.User.Commands;

public sealed record ChangeEmailCommand : RequestBase<UserResponse>
{
    public required Guid Id { get; set; }
    public required string NewEmail { get; set; }
    public required string Token { get; set; }
}

public sealed class ChangeEmailCommandValidator : AbstractValidator<ChangeEmailCommand>
{
    public ChangeEmailCommandValidator()
    {
        RuleFor(r => r.NewEmail)
            .MaximumLength(50)
            .EmailAddress()
            .Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
    }
}

public sealed class ChangeEmailCommandHandler(UserManager<UserEntity> userManager, IMapper mapper) : RequestHandlerBase<ChangeEmailCommand, UserResponse>
{
    public override async Task<UserResponse> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        var userEntity = await userManager.FindByIdAsync(request.Id.ToString())
            ?? throw new UserNotFoundException(request.Id);

        await userManager.ChangeEmailAsync(userEntity, request.NewEmail, request.Token);

        return mapper.Map<UserResponse>(userEntity);
    }
}
