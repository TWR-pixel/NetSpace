using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.Community.Caching;
using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.Community.Commands.PartiallyUpdate;

public sealed record PartiallyUpdateCommunityCommand : CommandBase<CommunityResponse>
{
    public required int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }

    public Guid? OwnerId { get; set; }

}

public sealed class PartiallyUpdateCommunityCommandValidator : AbstractValidator<PartiallyUpdateCommunityCommand>
{
    public PartiallyUpdateCommunityCommandValidator()
    {

    }
}

public sealed class PartiallyUpdateCommunityCommandHandler(IUnitOfWork unitOfWork,
                                                           ICommunityDistributedCache cache,
                                                           IMapper mapper) : CommandHandlerBase<PartiallyUpdateCommunityCommand, CommunityResponse>(unitOfWork)
{
    public override async Task<CommunityResponse> Handle(PartiallyUpdateCommunityCommand request, CancellationToken cancellationToken)
    {
        var communityEntity = await UnitOfWork.Communities.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityNotFoundException(request.Id);

        mapper.Map(request, communityEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);
        await cache.UpdateByIdAsync(communityEntity, communityEntity.Id, cancellationToken);

        return mapper.Map<CommunityResponse>(communityEntity);
    }
}
