using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.CommunitySubscription.Caching;
using NetSpace.Community.Domain.CommunitySubscription;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunitySubscription.Commands;

public sealed record CreateCommunitySubscriptionCommand : CommandBase<CommunitySubscriptionResponse>
{
    public Guid SubscriberId { get; set; }
    public int CommunityId { get; set; }

    public SubscribingStatus SubscribingStatus { get; set; } = SubscribingStatus.WaitForConfirmation;
}

public sealed class CreateCommunitySubscriptionCommandValidator : AbstractValidator<CreateCommunitySubscriptionCommand>
{
    public CreateCommunitySubscriptionCommandValidator()
    {

    }
}

public sealed class CreateCommunitySubscriptionCommandHandler(IUnitOfWork unitOfWork,
                                                              ICommunitySubscriptionDistributedCache cache,
                                                              IMapper mapper,
                                                              IValidator<CreateCommunitySubscriptionCommand> commandValidator) : CommandHandlerBase<CreateCommunitySubscriptionCommand, CommunitySubscriptionResponse>(unitOfWork)
{
    public override async Task<CommunitySubscriptionResponse> Handle(CreateCommunitySubscriptionCommand request, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var subscriptionEntity = mapper.Map<CommunitySubscriptionEntity>(request);

        await UnitOfWork.CommunitySubscriptions.AddAsync(subscriptionEntity);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        await cache.AddAsync(subscriptionEntity, cancellationToken);

        return mapper.Map<CommunitySubscriptionResponse>(request);
    }
}
