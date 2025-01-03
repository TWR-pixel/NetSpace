using NetSpace.Community.Application.Common.Exceptions;
using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.Application.Community.Mappers.Extensions;
using NetSpace.Community.UseCases.Community;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Application.Community.Requests;

public sealed record UpdateCommunityRequest : RequestBase<CommunityResponse>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }

    public required Guid OwnerId { get; set; }
}

public sealed class UpdateCommunityRequestHandler(ICommunityRepository communityRepository, IUserRepository userRepository) : RequestHandlerBase<UpdateCommunityRequest, CommunityResponse>
{
    public async override Task<CommunityResponse> Handle(UpdateCommunityRequest request, CancellationToken cancellationToken)
    {
        var communityEntity = await communityRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityNotFoundException(request.Id);

        var ownerEntity = await userRepository.FindByIdAsync(request.OwnerId, cancellationToken)
            ?? throw new UserNotFoundException(request.OwnerId);

        communityEntity.Owner = ownerEntity;
        communityEntity.Name = request.Name;
        communityEntity.Description = request.Description;
        communityEntity.AvatarUrl = request.AvatarUrl;

        return communityEntity.ToResponse();
    }
}
