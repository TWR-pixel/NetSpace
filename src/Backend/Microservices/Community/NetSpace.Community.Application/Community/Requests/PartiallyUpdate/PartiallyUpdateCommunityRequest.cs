using FluentValidation;
using NetSpace.Community.Application.Common.Exceptions;
using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.Application.Community.Mappers.Extensions;
using NetSpace.Community.UseCases.Community;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Application.Community.Requests.PartiallyUpdate;

public sealed record PartiallyUpdateCommunityRequest : RequestBase<CommunityResponse>
{
    public required int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }

    public Guid? OwnerId { get; set; }

}

public sealed class PartiallyUpdateCommunityRequestValidator : AbstractValidator<PartiallyUpdateCommunityRequest>
{
    public PartiallyUpdateCommunityRequestValidator()
    {

    }
}

public sealed class PartiallyUpdateCommunityRequestHandler(ICommunityRepository communityRepository, IUserRepository userRepository) : RequestHandlerBase<PartiallyUpdateCommunityRequest, CommunityResponse>
{
    public override async Task<CommunityResponse> Handle(PartiallyUpdateCommunityRequest request, CancellationToken cancellationToken)
    {
        var communityEntity = await communityRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityNotFoundException(request.Id);

        if (!string.IsNullOrWhiteSpace(request.Name))
            communityEntity.Name = request.Name;

        if (!string.IsNullOrWhiteSpace(request.Description))
            communityEntity.Description = request.Description;

        if (!string.IsNullOrWhiteSpace(request.AvatarUrl))
            communityEntity.AvatarUrl = request.AvatarUrl;

        if (request.OwnerId is not null)
        {
            var ownerEntity = await userRepository.FindByIdAsync((Guid)request.OwnerId, cancellationToken)
                ?? throw new UserNotFoundException((Guid)request.OwnerId);

            communityEntity.OwnerId = ownerEntity.Id;
            communityEntity.Owner = ownerEntity;
        }

        await communityRepository.UpdateAsync(communityEntity, cancellationToken);
        await communityRepository.SaveChangesAsync(cancellationToken);

        return communityEntity.ToResponse();
    }
}
