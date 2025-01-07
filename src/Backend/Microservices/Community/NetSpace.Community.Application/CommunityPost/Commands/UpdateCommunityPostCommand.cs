
using MapsterMapper;
using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.Application.CommunityPost.Caching;
using NetSpace.Community.Application.CommunityPost.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunityPost.Commands;

public sealed record UpdateCommunityPostCommand : CommandBase<CommunityPostResponse>
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }

    public required int CommunityId { get; set; }

}

public sealed class UpdateCommunityPostCommandHandler(IUnitOfWork unitOfWork, ICommunityPostDistributedCache cache, IMapper mapper) : CommandHandlerBase<UpdateCommunityPostCommand, CommunityPostResponse>(unitOfWork)
{
    public override async Task<CommunityPostResponse> Handle(UpdateCommunityPostCommand request, CancellationToken cancellationToken)
    {
        var communityPostEntity = await UnitOfWork.CommunityPosts.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityPostNotFoundException(request.Id);

        _ = await UnitOfWork.Communities.FindByIdAsync(request.CommunityId, cancellationToken)
            ?? throw new CommunityNotFoundException(request.CommunityId);

        mapper.Map(request, communityPostEntity);

        await cache.UpdateByIdAsync(communityPostEntity, communityPostEntity.Id, cancellationToken);

        return mapper.Map<CommunityPostResponse>(communityPostEntity);
    }
}
