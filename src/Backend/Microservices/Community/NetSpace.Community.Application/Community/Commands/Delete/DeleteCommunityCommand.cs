using MapsterMapper;
using NetSpace.Community.Application.Community.Caching;
using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.Community.Commands.Delete;

public sealed record DeleteCommunityCommand : CommandBase<CommunityResponse>
{
    public required int Id { get; set; }
}

public sealed class DeleteCommunityRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, ICommunityDistributedCache cache) : CommandHandlerBase<DeleteCommunityCommand, CommunityResponse>(unitOfWork)
{
    public override async Task<CommunityResponse> Handle(DeleteCommunityCommand request, CancellationToken cancellationToken)
    {
        var entity = await UnitOfWork.Communities.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityNotFoundException(request.Id);

        await UnitOfWork.Communities.DeleteAsync(entity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
        await cache.RemoveByIdAsync(request.Id, cancellationToken);

        return mapper.Map<CommunityResponse>(entity);
    }
}
