using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.Application.Community.Mappers.Extensions;
using NetSpace.Community.UseCases.Community;

namespace NetSpace.Community.Application.Community.Requests.Delete;

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
        await communityRepository.SaveChangesAsync(cancellationToken);

        return entity.ToResponse();
    }
}
