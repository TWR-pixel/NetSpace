using MapsterMapper;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.UseCases.CommunityPost;

namespace NetSpace.Community.Application.CommunityPost.Queries;

public sealed record GetCommunityPostQuery : QueryBase<IEnumerable<CommunityPostResponse>>
{
    public CommunityPostFilterOptions Filter { get; set; } = new CommunityPostFilterOptions();
    public PaginationOptions Pagination { get; set; } = new PaginationOptions();
    public SortOptions Sort { get; set; } = new SortOptions();
}

public sealed class GetCommunityPostQueryHandler(IReadonlyUnitOfWork unitOfWork, IMapper mapper) : QueryHandlerBase<GetCommunityPostQuery, IEnumerable<CommunityPostResponse>>(unitOfWork)
{
    public override async Task<IEnumerable<CommunityPostResponse>> Handle(GetCommunityPostQuery request, CancellationToken cancellationToken)
    {
        var result = await UnitOfWork.CommunityPosts.FilterAsync(request.Filter, request.Pagination, request.Sort, cancellationToken);

        return mapper.Map<IEnumerable<CommunityPostResponse>>(result ?? []);
    }
}
