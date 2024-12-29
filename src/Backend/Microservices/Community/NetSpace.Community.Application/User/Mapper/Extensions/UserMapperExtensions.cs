using NetSpace.Community.Domain;

namespace NetSpace.Community.Application.User.Mapper.Extensions;

public static class UserMapperExtensions
{
    public static UserResponse ToResponse(this UserEntity entity)
    {
        var response = new UserResponse(entity.Id, entity.Nickname, entity.Name, entity.Surname, entity.LastName, entity.About, entity.AvatarUrl, entity.BirthDate, entity.Gender);

        return response;
    }

    public static UserEntity ToEntity(this UserResponse response)
    {
        var entity = new UserEntity(response.Id, response.Nickname, response.Name, response.Surname, response.LastName, response.About, response.AvatarUrl, response.BirthDate, response.Gender);

        return entity;
    }


}
