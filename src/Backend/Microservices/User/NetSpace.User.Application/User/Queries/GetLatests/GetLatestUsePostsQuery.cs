using NetSpace.User.Application.UserPost;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.User.Queries.GetLatests;

public sealed record GetLatestUsePostsQuery : QueryBase<IEnumerable<UserPostResponse>>
{
    public PaginationOptions Pagination { get; set; } = new PaginationOptions();
}

public sealed class GetLatestUserPostsQueryHandler(IReadonlyUnitOfWork unitOfWork) : QueryHandlerBase<GetLatestUsePostsQuery, IEnumerable<UserPostResponse>>(unitOfWork)
{
    public override Task<IEnumerable<UserPostResponse>> Handle(GetLatestUsePostsQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
