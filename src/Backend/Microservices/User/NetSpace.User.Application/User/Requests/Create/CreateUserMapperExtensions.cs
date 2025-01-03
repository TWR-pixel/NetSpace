using NetSpace.User.Domain.User;

namespace NetSpace.User.Application.User.Requests.Create;

public static class CreateUserMapperExtensions
{
    public static UserEntity ToEntity(this CreateUserRequest request)
    {
        var entity = new UserEntity
        {
            Nickname = request.Nickname,
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            LastName = request.LastName,
            About = request.About,
            BirthDate = request.BirthDate,
            Hometown = request.Hometown,
            Language = request.Language,
            MaritalStatus = request.MaritalStatus,
            CurrentCity = request.CurrentCity,
            PersonalSite = request.PersonalSite,
            Gender = request.Gender,
            SchoolName = request.SchoolName,
        };

        return entity;
    }
}
