
using MapsterMapper;
using NetSpace.Community.Application.CommunitySubscription.Caching;
using NetSpace.Community.Application.CommunitySubscription.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunitySubscription.Queries;

public sealed record GetCommunitySubscriptionWitDetailsQuery : QueryBase<CommunitySubscriptionResponse>
{
    public required int Id { get; set; }
}

public sealed class GetCommunitySubscriptionWtihDetailsQueryHandler(IReadonlyUnitOfWork unitOfWork,
                                                                    ICommunitySubscriptionDistributedCache cache,
                                                                    IMapper mapper) : QueryHandlerBase<GetCommunitySubscriptionWitDetailsQuery, CommunitySubscriptionResponse>(unitOfWork)
{
    public override async Task<CommunitySubscriptionResponse> Handle(GetCommunitySubscriptionWitDetailsQuery request, CancellationToken cancellationToken)
    {
        var cachedSubscription = await cache.GetByIdAsync(request.Id, cancellationToken);

        if (cachedSubscription is null)
        {
            var subscriptionEntity = await UnitOfWork.CommunitySubscriptions.GetWithDetails(request.Id, cancellationToken)
                ?? throw new CommunitySubscriptionNotFoundException(request.Id);

            await cache.AddAsync(subscriptionEntity, cancellationToken);

            return mapper.Map<CommunitySubscriptionResponse>(subscriptionEntity);
        }


        return mapper.Map<CommunitySubscriptionResponse>(cachedSubscription);
    }
}

