using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Application.User.Extensions;
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

public sealed class PartiallyUpdateUserRequestHandler(IUserRepository userRepository) : RequestHandlerBase<PartiallyUpdateUserRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(PartiallyUpdateUserRequest request, CancellationToken cancellationToken)
    {
        var userEntity = await userRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserNotFoundException(request.Id);

        if (!string.IsNullOrWhiteSpace(request.Nickname))
            userEntity.Nickname = request.Nickname;

        if (!string.IsNullOrWhiteSpace(request.Nickname))
            userEntity.Nickname = request.Nickname;

        if (!string.IsNullOrWhiteSpace(request.Name))
            userEntity.Name = request.Name;

        if (!string.IsNullOrWhiteSpace(request.Surname))
            userEntity.Surname = request.Surname;

        if (!string.IsNullOrWhiteSpace(request.Email))
            userEntity.Email = request.Email;

        if (!string.IsNullOrWhiteSpace(request.LastName))
            userEntity.LastName = request.LastName;

        if (!string.IsNullOrWhiteSpace(request.About))
            userEntity.About = request.About;

        if (request.BirthDate is not null)
            userEntity.BirthDate = request.BirthDate;

        if (!string.IsNullOrWhiteSpace(request.About))
            userEntity.About = request.About;

        if (!string.IsNullOrWhiteSpace(request.Hometown))
            userEntity.Hometown = request.Hometown;

        if (request.Language is not null)
            userEntity.Language = (Language)request.Language;

        if (request.MaritalStatus is not null)
            userEntity.MaritalStatus = (MaritalStatus)request.MaritalStatus;

        if (!string.IsNullOrWhiteSpace(request.CurrentCity))
            userEntity.CurrentCity = request.CurrentCity;

        if (!string.IsNullOrWhiteSpace(request.PersonalSite))
            userEntity.PersonalSite = request.PersonalSite;

        if (request.Gender is not null)
            userEntity.Gender = (Gender)request.Gender;

        if (!string.IsNullOrWhiteSpace(request.SchoolName))
            userEntity.SchoolName = request.SchoolName;

        await userRepository.UpdateAsync(userEntity, cancellationToken);
        await userRepository.SaveChangesAsync(cancellationToken);

        return userEntity.ToResponse();
    }
}
