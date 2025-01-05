using FluentValidation;
using MapsterMapper;
using MassTransit;
using NetSpace.Common.Messages.User;
using NetSpace.User.Application.Common.Cache;
using NetSpace.User.Domain.User;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Application.User.Requests.Create;

public sealed record CreateUserRequest : RequestBase<UserResponse>
{
    public required string Nickname { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public required string Email { get; set; }
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

public sealed class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
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

public sealed class CreateUserRequestHandler(IPublishEndpoint publisher,
                                             IUserRepository userRepository,
                                             IUserDistributedCacheStorage cache,
                                             IMapper mapper,
                                             IValidator<CreateUserRequest> requestValidator) : RequestHandlerBase<CreateUserRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAndThrowAsync(request, cancellationToken);

        var userEntity = mapper.Map<UserEntity>(request);
        await userRepository.AddAsync(userEntity, cancellationToken);

        var userCreatedMessage = mapper.Map<UserCreatedMessage>(userEntity);

        await publisher.Publish(userCreatedMessage, cancellationToken);
        await cache.AddAsync(userEntity, cancellationToken);

        await userRepository.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserResponse>(userEntity);
    }
}
