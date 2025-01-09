using FluentValidation;
using MapsterMapper;
using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.User.Commands;

public sealed record PartiallyUpdateUserCommand : CommandBase<UserResponse>
{
    public required Guid Id { get; set; }
    public string? Nickname { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? LastName { get; set; }
    public string? About { get; set; }
    public DateTime? BirthDate { get; set; }

    public string? Hometown { get; set; }
    public Domain.User.Language? Language { get; set; }
    public Domain.User.MaritalStatus? MaritalStatus { get; set; }
    public string? CurrentCity { get; set; }
    public string? PersonalSite { get; set; }

    public Domain.User.Gender? Gender { get; set; }

    public string? SchoolName { get; set; }
}

public sealed class PartiallyUpdateUserCommandHandler(IUnitOfWork unitOfWork,
                                                      IValidator<PartiallyUpdateUserCommand> requestValidator,
                                                      IMapper mapper,
                                                      IPublishEndpoint publisher) : CommandHandlerBase<PartiallyUpdateUserCommand, UserResponse>(unitOfWork)
{
    public override async Task<UserResponse> Handle(PartiallyUpdateUserCommand request, CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAndThrowAsync(request, cancellationToken);

        var userEntity = await UnitOfWork.Users.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserNotFoundException(request.Id);

        mapper.Map(request, userEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);
        await publisher.Publish(mapper.Map<UserUpdatedMessage>(userEntity), cancellationToken);

        var response = mapper.Map<UserResponse>(userEntity);

        return response;
    }
}

public sealed class PartiallyUpdateUserCommandValidator : AbstractValidator<PartiallyUpdateUserCommand>
{
    public PartiallyUpdateUserCommandValidator()
    {
        RuleFor(r => r.Nickname)
            .MaximumLength(50)
            .NotEmpty()
            .When(r => r.Nickname is not null);

        RuleFor(r => r.Name)
            .MaximumLength(100)
            .NotEmpty()
            .When(r => r.Name is not null);

        RuleFor(r => r.Surname)
            .MaximumLength(100)
            .NotEmpty()
            .When(r => r.Surname is not null);

        RuleFor(r => r.LastName)
            .MaximumLength(50)
            .NotEmpty()
            .When(r => r.LastName is not null);

        RuleFor(r => r.About)
            .MaximumLength(512)
            .When(r => r.About is not null);

        RuleFor(r => r.Hometown)
            .MaximumLength(50)
            .When(r => r.Hometown is not null);

        RuleFor(r => r.CurrentCity)
            .MaximumLength(55)
            .When(r => r.CurrentCity is not null);

        RuleFor(r => r.SchoolName)
            .MaximumLength(50)
            .When(r => r.SchoolName is not null);
    }
}
