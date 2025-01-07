using MapsterMapper;
using NetSpace.User.Application.UserPost;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.User.Queries.GetLatests;

public sealed record GetLatestUsePostsQuery : QueryBase<IEnumerable<UserPostResponse>>
{
    public PaginationOptions Pagination { get; set; } = new PaginationOptions();
}

public sealed class GetLatestUserPostsQueryHandler(IReadonlyUnitOfWork unitOfWork, IUserPostDistributedCacheStorage cache, IMapper mapper)
    : QueryHandlerBase<GetLatestUsePostsQuery, IEnumerable<UserPostResponse>>(unitOfWork)
{
    public override async Task<IEnumerable<UserPostResponse>> Handle(GetLatestUsePostsQuery request, CancellationToken cancellationToken)
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
