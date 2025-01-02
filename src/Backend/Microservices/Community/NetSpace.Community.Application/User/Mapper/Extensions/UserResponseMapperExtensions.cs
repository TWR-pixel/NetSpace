using NetSpace.Community.Domain.User;

namespace NetSpace.Community.Application.User.Mapper.Extensions;

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

    public static UserEntity ToEntity(this UserResponse response)
    {
        var entity = new UserEntity
        {
            Id = response.Id,
            Nickname = response.Nickname,
            Name = response.Name,
            Surname = response.Surname,
            Email = response.Email,
            LastName = response.LastName,
            About = response.About,
            AvatarUrl = response.AvatarUrl,
            BirthDate = response.BirthDate,
            RegistrationDate = response.RegistrationDate,
            LastLoginAt = response.LastLoginAt,
            Hometown = response.Hometown,
            Language = response.Language,
            MaritalStatus = response.MaritalStatus,
            CurrentCity = response.CurrentCity,
            PersonalSite = response.PersonalSite,
            Gender = response.Gender,
            SchoolName = response.SchoolName,
        };

        return entity;
    }


}
