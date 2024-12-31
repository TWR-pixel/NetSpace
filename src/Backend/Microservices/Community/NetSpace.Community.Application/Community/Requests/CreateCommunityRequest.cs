using FluentValidation;
using NetSpace.Community.Application.Common.Exceptions;
using NetSpace.Community.Domain.Community;
using NetSpace.Community.UseCases.Community;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Application.Community.Requests;

public sealed record CreateCommunityRequest : RequestBase<CommunityResponse>
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? AvatarUrl { get; set; }

    public required Guid OwnerId { get; set; }
}

public sealed class CreateCommunityRequestValidator : AbstractValidator<CreateCommunityRequest>
{

}

public sealed class CreateCommunityRequestHandler(ICommunityRepository communityRepository,
                                                  IUserRepository userRepository,
                                                  IValidator<CreateCommunityRequest> validator) : RequestHandlerBase<CreateCommunityRequest, CommunityResponse>
{
    public override async Task<CommunityResponse> Handle(CreateCommunityRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var userEntity = await userRepository.FindByIdAsync(request.OwnerId.ToString(), cancellationToken)
            ?? throw new UserNotFoundException(request.OwnerId.ToString());

        var communityEntity = new CommunityEntity
        {
            Name = request.Name,
            Description = request.Description,
            OwnerId = request.OwnerId.ToString(),
            Owner = userEntity,
            AvatarUrl = request.AvatarUrl
        };

        var result = await communityRepository.AddAsync(communityEntity, cancellationToken);
        
        var response = new CommunityResponse();

       return response;
    }
}
