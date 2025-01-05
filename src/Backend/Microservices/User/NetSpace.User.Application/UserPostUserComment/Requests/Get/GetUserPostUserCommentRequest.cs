using MapsterMapper;
using NetSpace.User.UseCases;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment.Requests.Get;

public sealed record GetUserPostUserCommentRequest : RequestBase<IEnumerable<UserPostUserCommentResponse>>
{
    public UserPostUserCommentFilterOptions Filter { get; set; } = new();
    public PaginationOptions Pagination { get; set; } = new();
    public SortOptions Sort { get; set; } = new();
}

public sealed class GetUserPostUserCommentRequestHandler(IUserPostUserCommentRepository userCommentRepository, IMapper mapper) : RequestHandlerBase<GetUserPostUserCommentRequest, IEnumerable<UserPostUserCommentResponse>>
{
    public override async Task<IEnumerable<UserPostUserCommentResponse>> Handle(GetUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        var userComments = await userCommentRepository.FilterAsync(request.Filter, request.Pagination, request.Sort, cancellationToken);

        return mapper.Map<IEnumerable<UserPostUserCommentResponse>>(userComments);
    }
}
