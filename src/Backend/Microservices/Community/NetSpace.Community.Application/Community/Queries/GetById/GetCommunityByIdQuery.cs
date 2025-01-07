
using MapsterMapper;
using NetSpace.Community.Application.Community.Caching;
using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.Community.Queries.GetById;

public sealed record GetCommunityByIdQuery : QueryBase<CommunityResponse>
{
    public required int Id { get; set; }
}

public sealed class GetCommunityByIdQueryHandler(IReadonlyUnitOfWork unitOfWork, ICommunityDistributedCache cache, IMapper mapper) : QueryHandlerBase<GetCommunityByIdQuery, CommunityResponse>(unitOfWork)
{
    public override async Task<CommunityResponse> Handle(GetCommunityByIdQuery request, CancellationToken cancellationToken)
    {
        var cachedCommunityEntity = await cache.GetByIdAsync(request.Id, cancellationToken);

        if (cachedCommunityEntity is null)
        {
            var communityEntity = await UnitOfWork.Communities.GetByIdWithDetails(request.Id, cancellationToken)
                ?? throw new CommunityNotFoundException(request.Id);

            await cache.AddAsync(communityEntity, cancellationToken);

            return mapper.Map<CommunityResponse>(communityEntity);
        }

        return mapper.Map<CommunityResponse>(cachedCommunityEntity);
    }
}
