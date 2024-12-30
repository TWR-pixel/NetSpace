using NetSpace.Community.UseCases.Community;

namespace NetSpace.Community.Application.Community.Requests;

public sealed record UpdateCommunityRequest : RequestBase<CommunityResponse>
{
}

public sealed class UpdateCommunityRequestHandler(ICommunityRepository communityRepository) : RequestHandlerBase<UpdateCommunityRequest, CommunityResponse>
{
    public async override Task<CommunityResponse> Handle(UpdateCommunityRequest request, CancellationToken cancellationToken)
    {
        return new CommunityResponse();
    }
}
