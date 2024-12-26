using FluentValidation;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using NetSpace.Common.Application;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Domain;

namespace NetSpace.User.Application.User.Requests;

public sealed record RegisterUserRequest : RequestBase<RegisterUserResponse>
{
    public required string Nickname { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public string LastName { get; set; } = string.Empty;
    public required string Password { get; set; }
}

public sealed record RegisterUserResponse : ResponseBase
{

}

public sealed class RegisterUserRequestValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .NotNull()
            .EmailAddress()
            .Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");

        RuleFor(x => x.Password)
            .NotEmpty()
            .NotNull()
            .MinimumLength(6);
    }
}

public sealed class RegisterUserRequestHandler(UserManager<UserEntity> userManager,
                                               IValidator<RegisterUserRequest> validator,
                                               IPublishEndpoint publisher) : RequestHandlerBase<RegisterUserRequest, RegisterUserResponse>
{
    public override async Task<RegisterUserResponse> Handle(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var userEntity = new UserEntity(request.Nickname, request.Name, request.Surname, request.LastName);

        var userFromDb = await userManager.FindByEmailAsync(request.Email);

        if (userFromDb != null)
            throw new UserAlreadyExistsException(request.Email);

        await userManager.CreateAsync(userEntity, request.Password);

        await publisher.Publish(new UserCreatedMessage(userEntity.Id,
                                                       userEntity.Nickname,
                                                       userEntity.Name,
                                                       userEntity.Surname,
                                                       userEntity.Email,
                                                       userEntity.LastName,
                                                       userEntity.About,
                                                       userEntity.AvatarUrl,
                                                       (NetSpace.Common.Messages.User.Gender)userEntity.Gender), cancellationToken);

        return new RegisterUserResponse();
    }
}
