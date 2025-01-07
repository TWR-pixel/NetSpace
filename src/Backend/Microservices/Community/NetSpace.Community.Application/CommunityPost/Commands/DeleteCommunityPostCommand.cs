
using MapsterMapper;
using NetSpace.Community.Application.CommunityPost.Caching;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunityPost.Commands;

public sealed record DeleteCommunityPostCommand : CommandBase<CommunityPostResponse>
{
}

public sealed class DeleteCommunityPostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICommunityPostDistributedCache cache) : CommandHandlerBase<DeleteCommunityPostCommand, CommunityPostResponse>(unitOfWork)
{
    public override Task<CommunityPostResponse> Handle(DeleteCommunityPostCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
