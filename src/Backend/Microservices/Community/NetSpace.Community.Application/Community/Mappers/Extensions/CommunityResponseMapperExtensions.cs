using NetSpace.Community.Application.User.Mapper.Extensions;
using NetSpace.Community.Domain.Community;

namespace NetSpace.Community.Application.Community.Mappers.Extensions;

public static class CommunityResponseMapperExtensions
{
    public static CommunityResponse ToResponse(this CommunityEntity entity)
    {
        var response = new CommunityResponse
        {
            Id = entity.Id,
            Name = entity.Name,
            Description = entity.Description,
            AvatarUrl = entity.AvatarUrl,
            Owner = entity.Owner?.ToResponse(),
            OwnerId = entity.OwnerId,
            CreatedAt = entity.CreatedAt,
            LastNameUpdatedAt = entity.LastNameUpdatedAt,
        };

     
        return response;
    }

    public static IEnumerable<CommunityResponse> ToResponses(this IEnumerable<CommunityEntity> entities)
    {
        var responses = entities.Select(e => e.ToResponse());

        return responses;
    }
}
