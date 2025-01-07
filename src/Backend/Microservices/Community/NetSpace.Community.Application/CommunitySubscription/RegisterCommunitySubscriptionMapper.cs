using Mapster;
using NetSpace.Community.Application.CommunitySubscription.Commands;
using NetSpace.Community.Domain.CommunitySubscription;

namespace NetSpace.Community.Application.CommunitySubscription;

public sealed class RegisterCommunitySubscriptionMapper : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<CreateCommunitySubscriptionCommand, CommunitySubscriptionEntity>();
        config.NewConfig<DeleteCommunitySubscriptionCommand, CommunitySubscriptionEntity>();
        config.NewConfig<UpdateCommunitySubscriptionCommand, CommunitySubscriptionEntity>()
            .Ignore(c => c.Id);

        config.NewConfig<PartiallyUpdateCommunitySbuscriptionCommand, CommunitySubscriptionEntity>()
            .Ignore(c => c.Id)
            .IgnoreNullValues(true);
    }

}
