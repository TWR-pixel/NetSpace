using Mapster;
using NetSpace.Community.Application.Community.Requests.Create;
using NetSpace.Community.Application.Community.Requests.Update;
using NetSpace.Community.Domain.Community;

namespace NetSpace.Community.Application.Community;

public class RegisterCommunityMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCommunityRequest, CommunityEntity>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<CommunityEntity, CommunityResponse>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<UpdateCommunityRequest, CommunityEntity>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<IEnumerable<CommunityEntity>, IEnumerable<CommunityResponse>>()
            .RequireDestinationMemberSource(true);
    }
}
