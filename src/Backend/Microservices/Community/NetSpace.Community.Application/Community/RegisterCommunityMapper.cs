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
        config.NewConfig<CreateCommunityCommand, CommunityEntity>();
        config.NewConfig<CommunityEntity, CommunityResponse>();
        config.NewConfig<UpdateCommunityCommand, CommunityEntity>()
            .Ignore(p => p.Id);

        config.NewConfig<PartiallyUpdateCommunityCommand, CommunityEntity>()
            .IgnoreNullValues(true)
            .Ignore(p => p.Id);

        config.NewConfig<IEnumerable<CommunityEntity>, IEnumerable<CommunityResponse>>();
    }
}
