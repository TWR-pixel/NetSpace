using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.CommunitySubscription.Caching;
using NetSpace.Community.Application.CommunitySubscription.Exceptions;
using NetSpace.Community.Domain.CommunitySubscription;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunitySubscription.Commands;

public sealed record PartiallyUpdateCommunitySbuscriptionCommand : CommandBase<CommunitySubscriptionResponse>
{
    public int Id { get; set; }

    public Guid? SubscriberId { get; set; }
    public int? CommunityId { get; set; }

    public SubscribingStatus? SubscribingStatus { get; set; }
}

public sealed class PartiallyUpdateCommunitySbuscriptionCommandValidator : AbstractValidator<PartiallyUpdateCommunitySbuscriptionCommand>
{
    public PartiallyUpdateCommunitySbuscriptionCommandValidator()
    {

    }
}

public sealed class PartiallyUpdateCommunitySbuscriptionCommandHandler(IUnitOfWork unitOfWork,
                                                                       ICommunitySubscriptionDistributedCache cache,
                                                                       IMapper mapper,
                                                                       IValidator<PartiallyUpdateCommunitySbuscriptionCommand> commandValidator) : CommandHandlerBase<PartiallyUpdateCommunitySbuscriptionCommand, CommunitySubscriptionResponse>(unitOfWork)
{
    public override async Task<CommunitySubscriptionResponse> Handle(PartiallyUpdateCommunitySbuscriptionCommand request, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var subscriptionEntity = await UnitOfWork.CommunitySubscriptions.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunitySubscriptionNotFoundException(request.Id);

        mapper.Map(request, subscriptionEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CommunitySubscriptionResponse>(subscriptionEntity);
    }
}
