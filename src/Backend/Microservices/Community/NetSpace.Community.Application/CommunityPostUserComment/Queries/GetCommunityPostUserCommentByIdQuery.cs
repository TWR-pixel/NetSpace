using MapsterMapper;
using NetSpace.Community.Application.CommunityPostUserComment.Caching;
using NetSpace.Community.Application.CommunityPostUserComment.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunityPostUserComment.Queries;

public sealed record GetCommunityPostUserCommentByIdQuery : QueryBase<CommunityPostUserCommentResponse>
{
    public required int Id { get; set; }
}

public sealed class GetCommunityPostUserCommentByIdQueryHandler(IReadonlyUnitOfWork unitOfWork, IMapper mapper, ICommunityPostUserCommentDistributedCache cache) : QueryHandlerBase<GetCommunityPostUserCommentByIdQuery, CommunityPostUserCommentResponse>(unitOfWork)
{
    public override async Task<CommunityPostUserCommentResponse> Handle(GetCommunityPostUserCommentByIdQuery request, CancellationToken cancellationToken)
    {
        var cachedComment = await cache.GetByIdAsync(request.Id, cancellationToken);

        if (cachedComment is null)
        {

            var commentEntity = await UnitOfWork.CommunityPostUserComments.GetByIdWithDetails(request.Id, cancellationToken)
                ?? throw new CommunityPostUserCommentNotFoundException(request.Id);

            await cache.AddAsync(commentEntity, cancellationToken);

            return mapper.Map<CommunityPostUserCommentResponse>(commentEntity);
        }

        return mapper.Map<CommunityPostUserCommentResponse>(cachedComment);
    }
}
