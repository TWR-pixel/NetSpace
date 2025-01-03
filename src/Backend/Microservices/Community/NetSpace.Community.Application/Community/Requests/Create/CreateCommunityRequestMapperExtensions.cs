using NetSpace.Community.Domain.Community;
using NetSpace.Community.Domain.User;

namespace NetSpace.Community.Application.Community.Requests.Create;

public static class CreateCommunityRequestMapperExtensions
{
    public static CommunityEntity ToEntity(this CreateCommunityRequest request, UserEntity owner)
    {
        var entity = new CommunityEntity
        {
            Name = request.Name,
            Description = request.Description,
            AvatarUrl = request.AvatarUrl,
            Owner = owner,
            OwnerId = request.OwnerId,
        };

        return entity;
    }


}
