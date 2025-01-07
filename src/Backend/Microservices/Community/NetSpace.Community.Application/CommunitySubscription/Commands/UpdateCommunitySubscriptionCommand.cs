using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.CommunitySubscription.Exceptions;
using NetSpace.Community.Domain.CommunitySubscription;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunitySubscription.Commands;

public sealed record UpdateCommunitySubscriptionCommand : CommandBase<CommunitySubscriptionResponse>
{
    public required int Id { get; set; }

    public required Guid SubscriberId { get; set; }
    public required int CommunityId { get; set; }

    public SubscribingStatus SubscribingStatus { get; set; } 
}

public sealed class UpdateCommunitySbuscriptionCommandValidator : AbstractValidator<UpdateCommunitySubscriptionCommand>
{
    public UpdateCommunitySbuscriptionCommandValidator()
    {

    }
}

public sealed class UpdateCommunitySbuscriptionCommandHandler(IUnitOfWork unitOfWork,
                                                              IMapper mapper,
                                                              IValidator<UpdateCommunitySubscriptionCommand> commandValidator)
    : CommandHandlerBase<UpdateCommunitySubscriptionCommand, CommunitySubscriptionResponse>(unitOfWork)
{
    public override async Task<CommunitySubscriptionResponse> Handle(UpdateCommunitySubscriptionCommand request, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var subscriptionEntity = await UnitOfWork.CommunitySubscriptions.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunitySubscriptionNotFoundException(request.Id);

        mapper.Map(request, subscriptionEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CommunitySubscriptionResponse>(subscriptionEntity);
    }
}
