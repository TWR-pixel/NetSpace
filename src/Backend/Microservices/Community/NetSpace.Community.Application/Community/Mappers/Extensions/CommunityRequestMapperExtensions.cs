using NetSpace.Community.Domain.Community;
using NetSpace.Community.Domain.User;

namespace NetSpace.Community.Application.Community.Mappers.Extensions;

public static class CommunityRequestMapperExtensions
{
    public static CommunityEntity ToEntity(this CommunityRequest request, UserEntity owner)
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
