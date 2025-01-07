
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunitySubscription.Queries;

public sealed record GetCommunitySubscriptionWitDetailsQuery : QueryBase<CommunitySubscriptionResponse>
{
    public required int Id { get; set; }
}

public sealed class GetCommunitySubscriptionWtihDetailsQueryHandler(IReadonlyUnitOfWork unitOfWork) : QueryHandlerBase<GetCommunitySubscriptionWitDetailsQuery, CommunitySubscriptionResponse>(unitOfWork)
{
    public override Task<CommunitySubscriptionResponse> Handle(GetCommunitySubscriptionWitDetailsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

