﻿using FluentValidation;
using MapsterMapper;
using MassTransit;
using Microsoft.AspNetCore.Identity;
using NetSpace.Common.Messages.User;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Application.User.Commands;

public sealed record CreateUserCommand : RequestBase<UserResponse>
{
    public required string Nickname { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string About { get; set; } = string.Empty;
    public DateTime? BirthDate { get; set; }

    public string Hometown { get; set; } = string.Empty;
    public Domain.User.Language Language { get; set; } = Domain.User.Language.NotSet;
    public Domain.User.MaritalStatus MaritalStatus { get; set; } = Domain.User.MaritalStatus.NotSet;
    public string CurrentCity { get; set; } = string.Empty;
    public string PersonalSite { get; set; } = string.Empty;

    public Domain.User.Gender Gender { get; set; } = Domain.User.Gender.NotSet;

    public string SchoolName { get; set; } = string.Empty;
}

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(r => r.Nickname)
            .MaximumLength(50)
            .NotEmpty();

        RuleFor(r => r.Name)
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

        RuleFor(r => r.Hometown)
            .MaximumLength(50);

        RuleFor(r => r.CurrentCity)
            .MaximumLength(55);

        RuleFor(r => r.SchoolName)
            .MaximumLength(50);
    }
}

public sealed class CreateUserCommandHandler(IPublishEndpoint publisher,
                                             UserManager<UserEntity> userManager,
                                             IMapper mapper,
                                             IValidator<CreateUserCommand> requestValidator) : RequestHandlerBase<CreateUserCommand, UserResponse>
{
    public override async Task<UserResponse> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAndThrowAsync(request, cancellationToken);

        var userEntity = mapper.Map<UserEntity>(request);

        await userManager.CreateAsync(userEntity, request.Password);
        await publisher.Publish(mapper.Map<UserCreatedMessage>(userEntity), cancellationToken);

        return mapper.Map<UserResponse>(userEntity);
    }
}
