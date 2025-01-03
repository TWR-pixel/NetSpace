using NetSpace.Community.Application.Community.Mappers.Extensions;
using NetSpace.Community.Domain.CommunityPost;

namespace NetSpace.Community.Application.CommunityPost.Mappers;

public static class CommunityPostResponseMapperExtensions
{
    public static CommunityPostResponse ToResponse(this CommunityPostEntity entity)
    {
        var response = new CommunityPostResponse
        {
            Id = entity.Id,
            Title = entity.Title,
            Body = entity.Body,
            Community = entity.Community?.ToResponse(),
            CommunityId = entity.Id,
        };

        return response;
    }

    public static IEnumerable<CommunityPostResponse> ToResponses(this IEnumerable<CommunityPostEntity> entities)
    {
        var responses = entities.Select(e => e.ToResponse());

        return responses;
    }
}
