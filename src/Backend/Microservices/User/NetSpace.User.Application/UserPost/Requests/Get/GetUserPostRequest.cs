using NetSpace.User.Application.UserPost.Extensions;
using NetSpace.User.UseCases;
using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.Application.UserPost.Requests.Get;

public sealed record GetUserPostRequest : RequestBase<IEnumerable<UserPostResponse>>
{
    public UserPostFilterOptions Filter { get; set; } = new();
    public PaginationOptions Pagination { get; set; } = new();
    public SortOptions Sort { get; set; } = new();
}

public sealed class GetUserPostRequestHandler(IUserPostRepository userPostRepository) : RequestHandlerBase<GetUserPostRequest, IEnumerable<UserPostResponse>>
{
    public override async Task<IEnumerable<UserPostResponse>> Handle(GetUserPostRequest request, CancellationToken cancellationToken)
    {
        var result = await userPostRepository.FilterAsync(request.Filter, request.Pagination, request.Sort, cancellationToken);

        return result.ToResponses();
    }
}
