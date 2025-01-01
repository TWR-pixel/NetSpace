using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Application.User.Extensions;
using NetSpace.User.Domain.User;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Application.User.Requests;

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

public sealed record UpdateUserResponse : ResponseBase;

public sealed class UpdateUserRequestHandler(IUserRepository userRepository) : RequestHandlerBase<UpdateUserRequest, UserResponse>
{
    public override async Task<UserResponse> Handle(UpdateUserRequest request, CancellationToken cancellationToken)
    {
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

        return userEntity.ToResponse();
    }
}
