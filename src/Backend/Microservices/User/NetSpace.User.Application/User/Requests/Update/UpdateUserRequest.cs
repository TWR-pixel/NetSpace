using FluentValidation;
using MapsterMapper;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Domain.User;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Application.User.Requests.Update;

public sealed record UpdateUserRequest : RequestBase<UserResponse>
{
    public required Guid Id { get; set; }
    public string? Nickname { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? LastName { get; set; }
    public string? About { get; set; }
    public DateTime? BirthDate { get; set; }

    public string? Hometown { get; set; }
    public Language Language { get; set; }
    public MaritalStatus MaritalStatus { get; set; }
    public string? CurrentCity { get; set; }
    public string? PersonalSite { get; set; }

    public Gender Gender { get; set; }

    public string? SchoolName { get; set; }
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

        //RuleFor(r => r.Email)
        //    .MaximumLength(50)
        //    .EmailAddress()
        //    .Matches(@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$");

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

public sealed class UpdateUserRequestHandler(IUserRepository userRepository, IValidator<UpdateUserRequest> requestValidator, IMapper mapper) : RequestHandlerBase<UpdateUserRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAndThrowAsync(request, cancellationToken);

        var userEntity = await userRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserNotFoundException(request.Id);

        if (!string.IsNullOrWhiteSpace(request.Nickname))
            userEntity.Nickname = request.Nickname;

        if (!string.IsNullOrWhiteSpace(request.Name))
            userEntity.Name = request.Name;

        if (!string.IsNullOrWhiteSpace(request.Surname))
            userEntity.Surname = request.Surname;

        if (!string.IsNullOrWhiteSpace(request.LastName))
            userEntity.LastName = request.LastName;

        if (!string.IsNullOrWhiteSpace(request.About))
            userEntity.About = request.About;

        if (request.BirthDate is not null)
            userEntity.BirthDate = request.BirthDate;

        if (!string.IsNullOrWhiteSpace(request.Hometown))
            userEntity.Hometown = request.Hometown;

        if (request.Language != userEntity.Language)
            userEntity.Language = request.Language;

        if (request.MaritalStatus != userEntity.MaritalStatus)
            userEntity.MaritalStatus = request.MaritalStatus;

        if (request.Gender != userEntity.Gender)
            userEntity.Gender = request.Gender;

        if (!string.IsNullOrWhiteSpace(request.SchoolName))
            userEntity.SchoolName = request.SchoolName;

        await userRepository.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserResponse>(userEntity);
    }
}
