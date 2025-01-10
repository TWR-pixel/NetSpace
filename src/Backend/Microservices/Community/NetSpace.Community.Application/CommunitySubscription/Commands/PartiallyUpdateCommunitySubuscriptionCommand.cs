using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.CommunitySubscription.Exceptions;
using NetSpace.Community.Domain.CommunitySubscription;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunitySubscription.Commands;

public sealed record PartiallyUpdateCommunitySubuscriptionCommand : CommandBase<CommunitySubscriptionResponse>
{
    public required int Id { get; set; }

    public Guid? SubscriberId { get; set; }
    public int? CommunityId { get; set; }

    public SubscribingStatus? SubscribingStatus { get; set; }
}

public sealed class PartiallyUpdateCommunitySubscriptionValidator : AbstractValidator<PartiallyUpdateCommunitySubuscriptionCommand>
{
    public PartiallyUpdateCommunitySubscriptionValidator()
    {

    }
}

public sealed class PartiallyUpdateCommunitySubuscriptionCommandHandler(IUnitOfWork unitOfWork,
                                                                       IMapper mapper,
                                                                       IValidator<PartiallyUpdateCommunitySubuscriptionCommand> commandValidator) : CommandHandlerBase<PartiallyUpdateCommunitySubuscriptionCommand, CommunitySubscriptionResponse>(unitOfWork)
{
    public override async Task<CommunitySubscriptionResponse> Handle(PartiallyUpdateCommunitySubuscriptionCommand request, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var subscriptionEntity = await UnitOfWork.CommunitySubscriptions.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunitySubscriptionNotFoundException(request.Id);

        mapper.Map(request, subscriptionEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CommunitySubscriptionResponse>(subscriptionEntity);
    }
}
