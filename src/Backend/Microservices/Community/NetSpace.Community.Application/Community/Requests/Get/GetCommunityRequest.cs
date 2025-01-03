using NetSpace.Community.Application.Community.Mappers.Extensions;
using NetSpace.Community.Domain.Community;
using NetSpace.Community.UseCases;
using NetSpace.Community.UseCases.Community;

namespace NetSpace.Community.Application.Community.Requests.Get;

public sealed record GetCommunityRequest : RequestBase<IEnumerable<CommunityResponse>?>
{
    public CommunityFilterOptions Filter { get; set; } = new CommunityFilterOptions();
    public PaginationOptions Pagination { get; set; } = new PaginationOptions();
    public SortOptions Sort { get; set; } = new SortOptions();
}

public sealed class GetCommunityByIdRequestHandler(ICommunityRepository communityRepository) : RequestHandlerBase<GetCommunityRequest, IEnumerable<CommunityResponse>?>
{
    public override async Task<IEnumerable<CommunityResponse>?> Handle(GetCommunityRequest request, CancellationToken cancellationToken)
    {
        var result = await communityRepository.FilterAsync(request.Filter, request.Pagination, request.Sort, cancellationToken);

        return result.ToResponses();
    }
}
