using MapsterMapper;
using NetSpace.Community.UseCases.Common;
using NetSpace.Community.UseCases.CommunityPostUserComment;

namespace NetSpace.Community.Application.CommunityPostUserComment.Queries;

public sealed record GetCommunityPostUserCommentQuery : QueryBase<IEnumerable<CommunityPostUserCommentResponse>>
{
    public CommunityPostUsercommentFilterOptions Filter { get; set; } = new CommunityPostUsercommentFilterOptions();
    public PaginationOptions Pagination { get; set; } = new PaginationOptions();
    public SortOptions Sort { get; set; } = new SortOptions();
}

public sealed class GetCommunityPostUserCommentQueryHandler(IReadonlyUnitOfWork unitOfWork, IMapper mapper) : QueryHandlerBase<GetCommunityPostUserCommentQuery, IEnumerable<CommunityPostUserCommentResponse>>(unitOfWork)
{
    public override async Task<IEnumerable<CommunityPostUserCommentResponse>> Handle(GetCommunityPostUserCommentQuery request, CancellationToken cancellationToken)
    {
        var commentEntities = await UnitOfWork.CommunityPostUserComments.FilterAsync(request.Filter, request.Pagination, request.Sort, cancellationToken);

        return mapper.Map<IEnumerable<CommunityPostUserCommentResponse>>(commentEntities ?? []);
    }
}
