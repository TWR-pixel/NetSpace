using MapsterMapper;
using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.Community.Commands.Update;

public sealed record UpdateCommunityCommand : CommandBase<CommunityResponse>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }

    public required Guid OwnerId { get; set; }
}

public sealed class UpdateCommunityCommandHandler(IUnitOfWork unitOfWork,
                                                  IMapper mapper) : CommandHandlerBase<UpdateCommunityCommand, CommunityResponse>(unitOfWork)
{
    public async override Task<CommunityResponse> Handle(UpdateCommunityCommand request, CancellationToken cancellationToken)
    {
        var communityEntity = await UnitOfWork.Communities.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityNotFoundException(request.Id);

        mapper.Map(request, communityEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CommunityResponse>(communityEntity);
    }
}
