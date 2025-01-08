using FluentValidation;
using MapsterMapper;
using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.User.Commands.Update;

public sealed record UpdateUserCommand : CommandBase<UserResponse>
{
    public required Guid Id { get; set; }
    public required string Nickname { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? LastName { get; set; }
    public string? About { get; set; }
    public DateTime? BirthDate { get; set; }

    public string Hometown { get; set; } = string.Empty;
    public Domain.User.Language Language { get; set; } = Domain.User.Language.NotSet;
    public Domain.User.MaritalStatus MaritalStatus { get; set; } = Domain.User.MaritalStatus.NotSet;
    public string CurrentCity { get; set; } = string.Empty;
    public string PersonalSite { get; set; } = string.Empty;

    public Domain.User.Gender Gender { get; set; } = Domain.User.Gender.NotSet;

    public string SchoolName { get; set; } = string.Empty;
}

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
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

        RuleFor(r => r.LastName)
            .MaximumLength(50);

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

public sealed class UpdateUserCommandHandler(IUnitOfWork unitOfWork,
                                             IValidator<UpdateUserCommand> requestValidator,
                                             IMapper mapper,
                                             IPublishEndpoint publisher) : CommandHandlerBase<UpdateUserCommand, UserResponse>(unitOfWork)
{
    public override async Task<UserResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAndThrowAsync(request, cancellationToken);

        var userEntity = await UnitOfWork.Users.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserNotFoundException(request.Id);

        mapper.Map(request, userEntity);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
        await publisher.Publish(mapper.Map<UserUpdatedMessage>(userEntity), cancellationToken);

        return mapper.Map<UserResponse>(userEntity);
    }
}
