using Mapster;
using NetSpace.Community.Application.Community.Requests.Create;
using NetSpace.Community.Domain.Community;

namespace NetSpace.Community.Application.Community.Mappers;

public sealed class RegisterCommunityMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCommunityRequest, CommunityEntity>()
            .RequireDestinationMemberSource(true);

        config.NewConfig<CommunityEntity, CommunityResponse>()
            .RequireDestinationMemberSource(true);
    }
}
