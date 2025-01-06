using MapsterMapper;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.Application.UserPost.Queries.Get;

public sealed record GetUserPostQuery : QueryBase<IEnumerable<UserPostResponse>>
{
    public UserPostFilterOptions Filter { get; set; } = new();
    public PaginationOptions Pagination { get; set; } = new();
    public SortOptions Sort { get; set; } = new();
}

public sealed class GetUserPostQueryHandler(IReadonlyUnitOfWork unitOfWork, IMapper mapper) : QueryHandlerBase<GetUserPostQuery, IEnumerable<UserPostResponse>>(unitOfWork)
{
    public override async Task<IEnumerable<UserPostResponse>> Handle(GetUserPostQuery request, CancellationToken cancellationToken)
    {
        var result = await UnitOfWork.UserPosts.FilterAsync(request.Filter, request.Pagination, request.Sort, cancellationToken);

        return mapper.Map<IEnumerable<UserPostResponse>>(result);
    }
}
