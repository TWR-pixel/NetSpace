using MapsterMapper;
using NetSpace.Community.Application.CommunityPost.Caching;
using NetSpace.Community.Application.CommunityPost.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunityPost.Commands;

public sealed record DeleteCommunityPostCommand : CommandBase<CommunityPostResponse>
{
    public required int Id { get; set; }
}

public sealed class DeleteCommunityPostCommandHandler(IUnitOfWork unitOfWork,
                                                      IMapper mapper,
                                                      ICommunityPostDistributedCache cache) : CommandHandlerBase<DeleteCommunityPostCommand, CommunityPostResponse>(unitOfWork)
{
    public override async Task<CommunityPostResponse> Handle(DeleteCommunityPostCommand request, CancellationToken cancellationToken)
    {
        var communityPostEntity = await UnitOfWork.CommunityPosts.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityPostNotFoundException(request.Id);

        await UnitOfWork.CommunityPosts.DeleteAsync(communityPostEntity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
        await cache.RemoveByIdAsync(request.Id, cancellationToken);

        return mapper.Map<CommunityPostResponse>(communityPostEntity);
    }
}
