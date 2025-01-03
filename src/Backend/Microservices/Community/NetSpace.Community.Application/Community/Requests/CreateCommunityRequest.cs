using FluentValidation;
using NetSpace.Community.Application.Common.Exceptions;
using NetSpace.Community.Application.Community.Mappers.Extensions;
using NetSpace.Community.UseCases.Community;
using NetSpace.Community.UseCases.User;

namespace NetSpace.Community.Application.Community.Requests;

public sealed record CreateCommunityRequest : RequestBase<CommunityResponse>
{
    public required CommunityRequest CommunityRequest { get; set; }
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

        var communityRequest = request.CommunityRequest;

        var userEntity = await userRepository.FindByIdAsync(communityRequest.OwnerId, cancellationToken)
            ?? throw new UserNotFoundException(communityRequest.OwnerId);

        var communityEntity = communityRequest.ToEntity(userEntity);

        var result = await communityRepository.AddAsync(communityEntity, cancellationToken);
        await communityRepository.SaveChangesAsync(cancellationToken);

        var response = result.ToResponse();

        return response;
    }
}
