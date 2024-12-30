using NetSpace.User.Domain.User;

namespace NetSpace.User.Application.User.Extensions;

public static class UserRequestMapperExtensions
{
    public static UserEntity ToEntity(this UserRequest request)
    {
        var userEntity = new UserEntity(request.Nickname,
                                        request.Name,
                                        request.Surname,
                                        request.LastName,
                                        request.About,
                                        request.AvatarUrl,
                                        request.BirthDate,
                                        request.Gender);

        return userEntity;
    }

    public static UserRequest ToRequest(this UserEntity entity)
    {
        var userRequest = new UserRequest(
                                          entity.Nickname,
                                          entity.Name,
                                          entity.Surname,
                                          entity.LastName,
                                          entity.About,
                                          entity.AvatarUrl,
                                          BirthDate: entity.BirthDate,
                                          Gender: entity.Gender);

        return userRequest;
    }

}
