
using NetSpace.Community.Application.CommunityPost.Mappers;
using NetSpace.Community.UseCases;
using NetSpace.Community.UseCases.CommunityPost;

namespace NetSpace.Community.Application.CommunityPost.Requests;

public sealed record GetCommunityPostRequest : RequestBase<IEnumerable<CommunityPostResponse>>
{
    public CommunityPostFilterOptions Filter { get; set; } = new CommunityPostFilterOptions();
    public PaginationOptions Pagination { get; set; } = new PaginationOptions();
    public SortOptions Sort { get; set; } = new SortOptions();
}

public sealed class GetCommunityPostRequestHandler(ICommunityPostRepository communityPostRepository) : RequestHandlerBase<GetCommunityPostRequest, IEnumerable<CommunityPostResponse>>
{
    public override async Task<IEnumerable<CommunityPostResponse>> Handle(GetCommunityPostRequest request, CancellationToken cancellationToken)
    {
        var result = await communityPostRepository.FilterAsync(request.Filter, request.Pagination, request.Sort, cancellationToken);

        return result.ToResponses();
    }
}
