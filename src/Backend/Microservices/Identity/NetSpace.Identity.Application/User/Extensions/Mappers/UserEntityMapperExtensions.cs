using NetSpace.Common.Messages.User;
using NetSpace.Identity.Domain.User;

namespace NetSpace.Identity.Application.User.Extensions.Mappers;

public static class UserEntityMapperExtensions
{
    public static UserCreatedMessage ToCreated(this UserEntity entity)
    {
        var message = new UserCreatedMessage
        {
            
        };

        return message;
    }
}
