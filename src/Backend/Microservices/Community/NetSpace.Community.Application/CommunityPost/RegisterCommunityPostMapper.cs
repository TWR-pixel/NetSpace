using Mapster;
using NetSpace.Community.Application.Community;
using NetSpace.Community.Application.CommunityPost.Requests;
using NetSpace.Community.Domain.CommunityPost;

namespace NetSpace.Community.Application.CommunityPost;

public sealed class RegisterCommunityPostMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCommunityPostRequest, CommunityPostEntity>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<CommunityPostEntity, CommunityPostResponse>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<IEnumerable<CommunityPostEntity>, IEnumerable<CommunityResponse>>()
            .RequireDestinationMemberSource(true);
    }
}
