using Mapster;
using NetSpace.Community.Application.Community.Commands.Create;
using NetSpace.Community.Application.Community.Commands.PartiallyUpdate;
using NetSpace.Community.Application.Community.Commands.Update;
using NetSpace.Community.Domain.Community;

namespace NetSpace.Community.Application.Community;

public class RegisterCommunityMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCommunityCommand, CommunityEntity>()
            .RequireDestinationMemberSource(false);

        config.NewConfig<CommunityEntity, CommunityResponse>()
            .RequireDestinationMemberSource(false);

        config.NewConfig<UpdateCommunityCommand, CommunityEntity>()
            .RequireDestinationMemberSource(false);

        config.NewConfig<PartiallyUpdateCommunityCommand, CommunityEntity>()
            .IgnoreNullValues(true);

        config.NewConfig<IEnumerable<CommunityEntity>, IEnumerable<CommunityResponse>>()
            .RequireDestinationMemberSource(false);
    }
}
