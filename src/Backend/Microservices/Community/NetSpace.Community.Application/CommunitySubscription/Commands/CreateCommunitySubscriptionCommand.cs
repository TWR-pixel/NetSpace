using MapsterMapper;
using NetSpace.Community.Domain.CommunitySubscription;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunitySubscription.Commands;

public sealed record CreateCommunitySubscriptionCommand : CommandBase<CommunitySubscriptionResponse>
{
    public required Guid SubscriberId { get; set; }
    public required int CommunityId { get; set; }

    public SubscribingStatus SubscribingStatus { get; set; }
}

public sealed class CreateCommunitySubscriptionCommandHandler(IUnitOfWork unitOfWork,
                                                              IMapper mapper) : CommandHandlerBase<CreateCommunitySubscriptionCommand, CommunitySubscriptionResponse>(unitOfWork)
{
    public override async Task<CommunitySubscriptionResponse> Handle(CreateCommunitySubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscriptionEntity = mapper.Map<CommunitySubscriptionEntity>(request);

        await UnitOfWork.CommunitySubscriptions.AddAsync(subscriptionEntity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CommunitySubscriptionResponse>(request);
    }
}
