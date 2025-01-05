using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.Common;
using NetSpace.Community.Application.Common.Exceptions;
using NetSpace.Community.Domain.Community;
using NetSpace.Community.UseCases.Community;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Application.Community.Requests.Create;

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
                                                  IValidator<CreateCommunityRequest> validator,
                                                  IMapper mapper) : RequestHandlerBase<CreateCommunityRequest, CommunityResponse>
{
    public override async Task<CommunityResponse> Handle(CreateCommunityRequest request, CancellationToken cancellationToken)
    {
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var userEntity = await userRepository.FindByIdAsync(request.OwnerId, cancellationToken)
            ?? throw new UserNotFoundException(request.OwnerId);

        var communityEntity = mapper.Map<CommunityEntity>(request);

        var result = await communityRepository.AddAsync(communityEntity, cancellationToken);
        await communityRepository.SaveChangesAsync(cancellationToken);

        var response = mapper.Map<CommunityResponse>(result);

        return response;
    }
}
