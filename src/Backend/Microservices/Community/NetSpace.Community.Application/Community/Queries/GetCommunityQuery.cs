using MapsterMapper;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.UseCases.Community;

namespace NetSpace.Community.Application.Community.Queries;

public sealed record GetCommunityQuery : QueryBase<IEnumerable<CommunityResponse>?>
{
    public CommunityFilterOptions Filter { get; set; } = new CommunityFilterOptions();
    public PaginationOptions Pagination { get; set; } = new PaginationOptions();
    public SortOptions Sort { get; set; } = new SortOptions();
}

public sealed class GetCommunityQueryHandler(IReadonlyUnitOfWork unitOfWork, IMapper mapper) : QueryHandlerBase<GetCommunityQuery, IEnumerable<CommunityResponse>?>(unitOfWork)
{
    public override async Task<IEnumerable<CommunityResponse>?> Handle(GetCommunityQuery request, CancellationToken cancellationToken)
    {
        var result = await UnitOfWork.Communities.FilterAsync(request.Filter, request.Pagination, request.Sort, cancellationToken);

        return mapper.Map<IEnumerable<CommunityResponse>>(result ?? []);
    }
}
