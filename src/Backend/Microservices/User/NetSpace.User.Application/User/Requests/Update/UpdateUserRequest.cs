using FluentValidation;
using MapsterMapper;
using MediatR;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Domain.User;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Application.User.Requests.Update;

public sealed record UpdateUserRequest : RequestBase<UserResponse>
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

public sealed class UpdateUserRequestValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserRequestValidator()
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

public sealed class UpdateUserRequestHandler(IUserRepository userRepository,
                                             IValidator<UpdateUserRequest> requestValidator,
                                             IMapper mapper,
                                             IPublisher publisher) : RequestHandlerBase<UpdateUserRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAndThrowAsync(request, cancellationToken);

        var userEntity = await userRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserNotFoundException(request.Id);

        mapper.Map(request, userEntity);
        await userRepository.SaveChangesAsync(cancellationToken);
        await publisher.Publish(mapper.Map<UserUpdatedMessage>(userEntity), cancellationToken);

        return mapper.Map<UserResponse>(userEntity);
    }
}
