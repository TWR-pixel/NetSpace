using NetSpace.User.Domain.User;

namespace NetSpace.User.Application.User.Extensions;

public static class UserResponseMapperExtensions
{
    public static UserResponse ToResponse(this UserEntity entity)
    {
        var response = new UserResponse
        {
            Id = entity.Id,
            Nickname = entity.Nickname,
            Name = entity.Name,
            Surname = entity.Surname,
            Email = entity.Email,
            LastName = entity.LastName,
            About = entity.About,
            AvatarUrl = entity.AvatarUrl,
            BirthDate = entity.BirthDate,
            RegistrationDate = entity.RegistrationDate,
            LastLoginAt = entity.LastLoginAt,
            Hometown = entity.Hometown,
            Language = entity.Language,
            MaritalStatus = entity.MaritalStatus,
            CurrentCity = entity.CurrentCity,
            PersonalSite = entity.PersonalSite,
            Gender = entity.Gender,
            SchoolName = entity.SchoolName,
        };

        return response;
    }

    public static IEnumerable<UserResponse> ToResponses(this IEnumerable<UserEntity> entities)
    {
        var responses = entities.Select(e => e.ToResponse());

        return responses;
    }
}
