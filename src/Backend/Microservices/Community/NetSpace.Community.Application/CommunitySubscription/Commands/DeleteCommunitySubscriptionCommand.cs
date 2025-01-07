using MapsterMapper;
using NetSpace.Community.Application.CommunitySubscription.Caching;
using NetSpace.Community.Application.CommunitySubscription.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunitySubscription.Commands;

public sealed record DeleteCommunitySubscriptionCommand : CommandBase<CommunitySubscriptionResponse>
{
    public required int Id { get; set; }
}

public sealed class DeleteCommunitySubscriptionCommandHandler(IUnitOfWork unitOfWork,
                                                              ICommunitySubscriptionDistributedCache cache,
                                                              IMapper mapper) : CommandHandlerBase<DeleteCommunitySubscriptionCommand, CommunitySubscriptionResponse>(unitOfWork)
{
    public override async Task<CommunitySubscriptionResponse> Handle(DeleteCommunitySubscriptionCommand request, CancellationToken cancellationToken)
    {
        var subscriptionEntity = await UnitOfWork.CommunitySubscriptions.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunitySubscriptionNotFoundException(request.Id);

        await UnitOfWork.CommunitySubscriptions.DeleteAsync(subscriptionEntity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        await cache.RemoveByIdAsync(subscriptionEntity.Id, cancellationToken);

        return mapper.Map<CommunitySubscriptionResponse>(subscriptionEntity);
    }
}
