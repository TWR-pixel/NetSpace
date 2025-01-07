using MapsterMapper;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.UseCases.CommunitySubscription;

namespace NetSpace.Community.Application.CommunitySubscription.Queries;

public sealed record GetCommunitySubscriptionQuery : QueryBase<IEnumerable<CommunitySubscriptionResponse>>
{
    public CommunitySubscriptionFilterOptions Filter { get; set; } = new CommunitySubscriptionFilterOptions();
    public PaginationOptions Pagination { get; set; } = new PaginationOptions();
    public SortOptions Sort { get; set; } = new SortOptions();
}

public sealed class GetCommunitySubscriptionQueryHandler(IReadonlyUnitOfWork unitOfWork, IMapper mapper) : QueryHandlerBase<GetCommunitySubscriptionQuery, IEnumerable<CommunitySubscriptionResponse>>(unitOfWork)
{
    public override async Task<IEnumerable<CommunitySubscriptionResponse>> Handle(GetCommunitySubscriptionQuery request, CancellationToken cancellationToken)
    {
        var subscriptions = await UnitOfWork.CommunitySubscriptions.Filter(request.Filter, request.Pagination, request.Sort, cancellationToken);

        return mapper.Map<IEnumerable<CommunitySubscriptionResponse>>(subscriptions);
    }
}
