using MapsterMapper;
using NetSpace.Community.Application.CommunityPost.Caching;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunityPost.Queries;

public sealed record GetLatestCommunityPostsQuery : QueryBase<IEnumerable<CommunityPostResponse>>
{
    public PaginationOptions Pagination { get; set; } = new PaginationOptions();
}

public sealed class GetLatestCommunityPostsQueryHandler(IReadonlyUnitOfWork unitOfWork, ICommunityPostDistributedCache cache, IMapper mapper)
    : QueryHandlerBase<GetLatestCommunityPostsQuery, IEnumerable<CommunityPostResponse>>(unitOfWork)
{
    public override async Task<IEnumerable<CommunityPostResponse>> Handle(GetLatestCommunityPostsQuery request, CancellationToken cancellationToken)
    {
        var cachedCommunityPostEntities = await cache.GetLatest(cancellationToken);

        if (cachedCommunityPostEntities is null)
        {
            cachedCommunityPostEntities = await UnitOfWork.CommunityPosts.GetLatest(request.Pagination, cancellationToken);

            await cache.SetLatest(cachedCommunityPostEntities, cancellationToken);
        }

        return mapper.Map<IEnumerable<CommunityPostResponse>>(cachedCommunityPostEntities);
    }
}
