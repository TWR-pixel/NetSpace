using MapsterMapper;
using NetSpace.Community.Application.Common;
using NetSpace.Community.UseCases;
using NetSpace.Community.UseCases.Community;

namespace NetSpace.Community.Application.Community.Requests.Get;

public sealed record GetCommunityRequest : RequestBase<IEnumerable<CommunityResponse>?>
{
    public CommunityFilterOptions Filter { get; set; } = new CommunityFilterOptions();
    public PaginationOptions Pagination { get; set; } = new PaginationOptions();
    public SortOptions Sort { get; set; } = new SortOptions();
}

public sealed class GetCommunityByIdRequestHandler(ICommunityRepository communityRepository, IMapper mapper) : RequestHandlerBase<GetCommunityRequest, IEnumerable<CommunityResponse>?>
{
    public override async Task<IEnumerable<CommunityResponse>?> Handle(GetCommunityRequest request, CancellationToken cancellationToken)
    {
        var result = await communityRepository.FilterAsync(request.Filter, request.Pagination, request.Sort, cancellationToken);

        return mapper.Map<IEnumerable<CommunityResponse>>(result ?? []);
    }
}
