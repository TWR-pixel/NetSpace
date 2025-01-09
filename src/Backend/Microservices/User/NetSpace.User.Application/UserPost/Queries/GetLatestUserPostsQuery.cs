using MapsterMapper;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.UserPost.Queries;

public sealed record GetLatestUserPostsQuery : QueryBase<IEnumerable<UserPostResponse>>
{
    public PaginationOptions Pagination { get; set; } = new PaginationOptions();
}

public sealed class GetLatestUserPostsQueryHandler(IReadonlyUnitOfWork unitOfWork, IUserPostDistributedCacheStorage cache, IMapper mapper)
    : QueryHandlerBase<GetLatestUserPostsQuery, IEnumerable<UserPostResponse>>(unitOfWork)
{
    public override async Task<IEnumerable<UserPostResponse>> Handle(GetLatestUserPostsQuery request, CancellationToken cancellationToken)
    {
        var cached = await cache.GetLatest(cancellationToken);

        if (cached is null)
        {
            cached = await UnitOfWork.UserPosts.GetLatest(request.Pagination, cancellationToken);

            await cache.SetLatest(cached, cancellationToken);
        }

        return mapper.Map<IEnumerable<UserPostResponse>>(cached);
    }
}
