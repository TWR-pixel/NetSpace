using NetSpace.Common.Application;
using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.UseCases;

namespace NetSpace.Community.Application.Community.Requests;

public sealed record DeleteCommunityRequest : RequestBase<CommunityResponse>
{
    public required int Id { get; set; }
}

public sealed class DeleteCommunityRequestHandler(ICommunityRepository communityRepository) : RequestHandlerBase<DeleteCommunityRequest, CommunityResponse>
{
    public override async Task<CommunityResponse> Handle(DeleteCommunityRequest request, CancellationToken cancellationToken)
    {
        var entity = await communityRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityNotFoundException(request.Id);

        await communityRepository.DeleteAsync(entity, cancellationToken);

        return new CommunityResponse();
    }
}
