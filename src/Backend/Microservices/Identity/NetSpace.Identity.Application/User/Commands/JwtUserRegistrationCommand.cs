using FluentValidation;
using MapsterMapper;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using NetSpace.Common.Messages.User;
using NetSpace.Identity.Application.Common.Jwt;
using NetSpace.Identity.Application.User.Exceptions;
using NetSpace.Identity.Domain.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace NetSpace.Identity.Application.User.Commands;

public sealed record JwtUserRegistrationCommand : RequestBase<AccessTokenResponse>
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Nickname { get; set; }
    public required string UserName { get; set; }
    public required string Surname { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;

    public DateTime? BirthDate { get; set; }
    public Domain.User.Gender Gender { get; set; } = Domain.User.Gender.NotSet;
    public Domain.User.Language Language { get; set; } = Domain.User.Language.NotSet;
    public Domain.User.MaritalStatus MaritalStatus { get; set; } = Domain.User.MaritalStatus.NotSet;
}

public sealed class JwtUserRegistrationCommandValidator : AbstractValidator<JwtUserRegistrationCommand>
{
    public JwtUserRegistrationCommandValidator()
    {
        RuleFor(r => r.Nickname)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(r => r.UserName)
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(r => r.Surname)
            .MaximumLength(100)
            .NotEmpty();

        RuleFor(r => r.Email)
            .MaximumLength(50)
            .EmailAddress()
            .Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");

        RuleFor(r => r.LastName)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(r => r.About)
            .MaximumLength(512);
    }
}


public sealed class JwtUserRegistrationCommandHandler(UserManager<UserEntity> userManager,
                                                      RoleManager<IdentityRole> roleManager,
                                                      AccessTokenFactory tokenFactory,
                                                      IPublishEndpoint publisher,
                                                      IValidator<JwtUserRegistrationCommand> commandValidator,
                                                      IMapper mapper,
                                                      IEmailSender emailSender) : RequestHandlerBase<JwtUserRegistrationCommand, AccessTokenResponse>
{
    public override async Task<AccessTokenResponse> Handle(JwtUserRegistrationCommand request, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var userEntity = await userManager.FindByEmailAsync(request.Email);
        if (userEntity != null)
            throw new UserAlreadyExistsException(request.Email);

        userEntity = mapper.Map<UserEntity>(request);

        var createUserResult = await userManager.CreateAsync(userEntity, request.Password);
        if (!createUserResult.Succeeded)
            throw new UnauthorizedAccessException(createUserResult.Errors.First().Description);

        if (!await roleManager.RoleExistsAsync(UserRoles.User))
            await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

        await userManager.AddToRoleAsync(userEntity, UserRoles.User);

        var userRoles = await userManager.GetRolesAsync(userEntity);
        var authClaims = new List<Claim>
            {
               new(ClaimTypes.NameIdentifier, userEntity.Id),
               new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

        foreach (var userRole in userRoles)
        {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole));
        }

        var response = tokenFactory.GenerateToken(authClaims);

        await publisher.Publish(mapper.Map<UserCreatedMessage>(userEntity), cancellationToken);

        var confirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(userEntity);
        await emailSender.SendEmailAsync(userEntity.Email!, "Confirm email", confirmationToken);

        return response;
    }
}