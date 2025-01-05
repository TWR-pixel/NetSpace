using MapsterMapper;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment.Queries.Get;

public sealed record GetUserPostUserCommentQuery : QueryBase<IEnumerable<UserPostUserCommentResponse>>
{
    public UserPostUserCommentFilterOptions Filter { get; set; } = new();
    public PaginationOptions Pagination { get; set; } = new();
    public SortOptions Sort { get; set; } = new();
}

public sealed class GetUserPostUserCommentRequestHandler(IReadonlyUnitOfWork unitOfWork, IMapper mapper) 
    : QueryHandlerBase<GetUserPostUserCommentQuery, IEnumerable<UserPostUserCommentResponse>>(unitOfWork)
{
    public override async Task<IEnumerable<UserPostUserCommentResponse>> Handle(GetUserPostUserCommentQuery request, CancellationToken cancellationToken)
    {
        var userComments = await UnitOfWork.UserPostUserComments.FilterAsync(request.Filter, request.Pagination, request.Sort, cancellationToken);

        return mapper.Map<IEnumerable<UserPostUserCommentResponse>>(userComments);
    }
}
