using NetSpace.Community.Domain.Community;
using NetSpace.Community.UseCases.Community;

namespace NetSpace.Community.Application.Community.Requests;

public sealed record GetCommunityRequest : RequestBase<IEnumerable<CommunityEntity>?>
{
    public required FilterOptions Filter { get; set; }
}

public sealed class GetCommunityByIdRequestHandler(ICommunityRepository communityRepository) : RequestHandlerBase<GetCommunityRequest, IEnumerable<CommunityEntity>?>
{
    public override async Task<IEnumerable<CommunityEntity>?> Handle(GetCommunityRequest request, CancellationToken cancellationToken)
    {
        var result = await communityRepository.FilterAsync(request.Filter, cancellationToken);

        var response = result;

        return response;
    }
}
