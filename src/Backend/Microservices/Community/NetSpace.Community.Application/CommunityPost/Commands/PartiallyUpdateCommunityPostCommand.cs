using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.CommunityPost.Caching;
using NetSpace.Community.Application.CommunityPost.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunityPost.Commands;

public sealed record PartiallyUpdateCommunityPostCommand : CommandBase<CommunityPostResponse>
{
    public required int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
}

public sealed class PartiallyUpdateCommunityPostCommandValidator : AbstractValidator<PartiallyUpdateCommunityPostCommand>
{
    public PartiallyUpdateCommunityPostCommandValidator()
    {

    }
}


public sealed class PartiallyUpdateCommunityPostCommandHandler(IUnitOfWork unitOfWork, ICommunityPostDistributedCache cache, IMapper mapper) : CommandHandlerBase<PartiallyUpdateCommunityPostCommand, CommunityPostResponse>(unitOfWork)
{
    public override async Task<CommunityPostResponse> Handle(PartiallyUpdateCommunityPostCommand request, CancellationToken cancellationToken)
    {
        var communityPostEntity = await UnitOfWork.CommunityPosts.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityPostNotFoundException(request.Id);

        mapper.Map(request, communityPostEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);
        await cache.UpdateByIdAsync(communityPostEntity, communityPostEntity.Id, cancellationToken);

        return mapper.Map<CommunityPostResponse>(communityPostEntity);
    }
}
