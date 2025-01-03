using NetSpace.Common.Messages.User;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Application.User.Extensions.Mappers;

public static class UserEntityMapperExtensions
{
    public static UserCreatedMessage ToCreated(this UserEntity entity)
    {
        var message = new UserCreatedMessage
        {
            Id = Guid.Parse(entity.Id),
            Nickname = entity.Nickname,
            UserName = entity.UserName!,
            Surname = entity.Surname,
            Email = entity.Email!,
            LastName = entity.LastName,
            About = entity.About,
            AvatarUrl = entity.AvatarUrl,
            BirthDate = entity.BirthDate,
            RegistrationDate = entity.RegistrationDate,
            LastLoginAt = entity.LastLoginAt,
            Hometown = entity.Hometown,
            Language = (NetSpace.Common.Messages.User.Language)entity.Language,
            MaritalStatus = (NetSpace.Common.Messages.User.MaritalStatus)entity.MaritalStatus,
            CurrentCity = entity.CurrentCity,
            PersonalSite = entity.PersonalSite,
            Gender = (NetSpace.Common.Messages.User.Gender)entity.Gender,
            SchoolName = entity.SchoolName,
        };

        return message;
    }
}
