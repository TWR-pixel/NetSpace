using FluentValidation;
using MapsterMapper;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Domain.User;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Application.User.Requests.PartiallyUpdate;

public sealed record PartiallyUpdateUserRequest : RequestBase<UserResponse>
{
    public required Guid Id { get; set; }
    public string? Nickname { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Email { get; set; }
    public string? LastName { get; set; }
    public string? About { get; set; }
    public DateTime? BirthDate { get; set; }

    public string? Hometown { get; set; }
    public Language? Language { get; set; }
    public MaritalStatus? MaritalStatus { get; set; }
    public string? CurrentCity { get; set; }
    public string? PersonalSite { get; set; }

    public Gender? Gender { get; set; }

    public string? SchoolName { get; set; }
}

public sealed class PartiallyUpdateUserRequestValidator : AbstractValidator<PartiallyUpdateUserRequest>
{
    public PartiallyUpdateUserRequestValidator()
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

        RuleFor(r => r.Email)
            .MaximumLength(50)
            .EmailAddress()
            .Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$")
            .When(r => r.Email is not null);

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

public sealed class PartiallyUpdateUserRequestHandler(IUserRepository userRepository,
                                                      IValidator<PartiallyUpdateUserRequest> requestValidator,
                                                      IMapper mapper) : RequestHandlerBase<PartiallyUpdateUserRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(PartiallyUpdateUserRequest request, CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAndThrowAsync(request, cancellationToken);

        var userEntity = await userRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserNotFoundException(request.Id);
        
        mapper.Map(request, userEntity);

        await userRepository.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<UserResponse>(userEntity);

        return response;
    }
}
