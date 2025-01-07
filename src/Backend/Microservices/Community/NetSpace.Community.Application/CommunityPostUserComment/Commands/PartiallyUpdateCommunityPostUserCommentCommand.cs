using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.CommunityPost.Exceptions;
using NetSpace.Community.Application.CommunityPostUserComment.Caching;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunityPostUserComment.Commands;

public sealed record PartiallyUpdateCommunityPostUserCommentCommand : CommandBase<CommunityPostUserCommentResponse>
{
    public required int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
}

public sealed class PartiallyUpdateCommunityPostUserCommentCommandValidator : AbstractValidator<PartiallyUpdateCommunityPostUserCommentCommand>
{
    public PartiallyUpdateCommunityPostUserCommentCommandValidator()
    {

    }
}


public sealed class PartiallyUpdateCommunityPostUserCommentCommandHandler(IUnitOfWork unitOfWork, ICommunityPostUserCommentDistributedCache cache, IMapper mapper) 
    : CommandHandlerBase<PartiallyUpdateCommunityPostUserCommentCommand, CommunityPostUserCommentResponse>(unitOfWork)
{
    public override async Task<CommunityPostUserCommentResponse> Handle(PartiallyUpdateCommunityPostUserCommentCommand request, CancellationToken cancellationToken)
    {
        var commentEntity = await UnitOfWork.CommunityPostUserComments.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityPostNotFoundException(request.Id);

        mapper.Map(request, commentEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);
        await cache.UpdateByIdAsync(commentEntity, commentEntity.Id, cancellationToken);

        return mapper.Map<CommunityPostUserCommentResponse>(commentEntity);
    }
}
