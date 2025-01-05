
using FluentValidation;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;
using NetSpace.Identity.Application.User.Exceptions;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Application.User.Requests;

public sealed record ChangeEmailRequest : RequestBase<UserResponse>
{
    public required Guid Id { get; set; }
    public required string NewEmail { get; set; }
}

public sealed class ChangeEmailRequetsValidator : AbstractValidator<ChangeEmailRequest>
{
    public ChangeEmailRequetsValidator()
    {
        RuleFor(r => r.NewEmail)
            .MaximumLength(50)
            .EmailAddress()
            .Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");
    }
}

public sealed class ChangeEmailRequestHandler(UserManager<UserEntity> userManager, IMapper mapper) : RequestHandlerBase<ChangeEmailRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(ChangeEmailRequest request, CancellationToken cancellationToken)
    {
        var userEntity = await userManager.FindByIdAsync(request.Id.ToString())
            ?? throw new UserNotFoundException(request.Id);

        //await userManager.ChangeEmailAsync();

        return mapper.Map<UserResponse>(userEntity);
    }
}
