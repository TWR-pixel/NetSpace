using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.Community.Commands.PartiallyUpdate;

public sealed record PartiallyUpdateCommunityCommand : CommandBase<CommunityResponse>
{
    public required int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }

    public Guid? OwnerId { get; set; }

}

public sealed class PartiallyUpdateCommunityCommandValidator : AbstractValidator<PartiallyUpdateCommunityCommand>
{
    public PartiallyUpdateCommunityCommandValidator()
    {
        RuleFor(c => c.Name)
            .NotEmpty()
            .NotNull()
            .MaximumLength(256)
            .When(c => c.Name is not null);

        RuleFor(c => c.Description)
            .MaximumLength(512)
            .When(c => c.Description is not null);

        RuleFor(c => c.OwnerId)
            .NotNull()
            .NotEmpty()
            .When(c => c.OwnerId is not null);
    }
}

public sealed class PartiallyUpdateCommunityCommandHandler(IUnitOfWork unitOfWork,
                                                           IMapper mapper,
                                                           IValidator<PartiallyUpdateCommunityCommand> commandValidator) : CommandHandlerBase<PartiallyUpdateCommunityCommand, CommunityResponse>(unitOfWork)
{
    public override async Task<CommunityResponse> Handle(PartiallyUpdateCommunityCommand request, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var communityEntity = await UnitOfWork.Communities.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityNotFoundException(request.Id);

        if (!string.IsNullOrWhiteSpace(request.Name))
            communityEntity.LastNameUpdatedAt = DateTime.UtcNow;

        mapper.Map(request, communityEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CommunityResponse>(communityEntity);
    }
}
