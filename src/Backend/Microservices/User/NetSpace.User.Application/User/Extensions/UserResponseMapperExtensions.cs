using NetSpace.User.Domain;

namespace NetSpace.User.Application.User.Extensions;

public static class UserResponseMapperExtensions
{
    public static UserResponse ToResponse(this UserEntity entity)
    {
        var response = new UserResponse(entity.Nickname,
                                        entity.Name,
                                        entity.Surname,
                                        entity.LastName,
                                        entity.About,
                                        entity.AvatarUrl,
                                        BirthDate: entity.BirthDate,
                                        Gender: entity.Gender);

        return response;
    }
}
