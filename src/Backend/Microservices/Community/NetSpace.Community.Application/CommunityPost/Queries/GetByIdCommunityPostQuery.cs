
using MapsterMapper;
using NetSpace.Community.Application.CommunityPost.Caching;
using NetSpace.Community.Application.CommunityPost.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunityPost.Queries;

public sealed record GetByIdCommunityPostQuery : QueryBase<CommunityPostResponse>
{
    public required int Id { get; set; }
}

public sealed class GetByIdCommunityPostQueryHandler(IReadonlyUnitOfWork unitOfWork, IMapper mapper, ICommunityPostDistributedCache cache) : QueryHandlerBase<GetByIdCommunityPostQuery, CommunityPostResponse>(unitOfWork)
{
    public override async Task<CommunityPostResponse> Handle(GetByIdCommunityPostQuery request, CancellationToken cancellationToken)
    {
        var cachedData = await cache.GetByIdAsync(request.Id, cancellationToken);

        if (cachedData is null)
        {
            var communityPostEntity = await UnitOfWork.CommunityPosts.GetByIdWithDetails(request.Id, cancellationToken)
                ?? throw new CommunityPostNotFoundException(request.Id);

            await cache.AddAsync(communityPostEntity, cancellationToken);

            return mapper.Map<CommunityPostResponse>(communityPostEntity);
        }

        return mapper.Map<CommunityPostResponse>(cachedData);
    }
}
