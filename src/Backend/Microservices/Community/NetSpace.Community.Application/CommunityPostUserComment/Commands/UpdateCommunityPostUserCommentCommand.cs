using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.CommunityPostUserComment.Caching;
using NetSpace.Community.Application.CommunityPostUserComment.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunityPostUserComment.Commands;

public sealed record UpdateCommunityPostUserCommentCommand : CommandBase<CommunityPostUserCommentResponse>
{
    public required int Id { get; set; }
    public required string Body { get; set; }

    public required Guid OwnerId { get; set; }
    public required int CommunityPostId { get; set; }
}

public sealed class UpdateCommunityPostUserCommentCommandValidator : AbstractValidator<UpdateCommunityPostUserCommentCommand>
{
    public UpdateCommunityPostUserCommentCommandValidator()
    {

    }
}

public sealed class UpdateCommunityPostUserCommentCommandHandler(IUnitOfWork unitOfWork,
                                                                 ICommunityPostUserCommentDistributedCache cache,
                                                                 IMapper mapper,
                                                                 IValidator<UpdateCommunityPostUserCommentCommand> commandValidator) : CommandHandlerBase<UpdateCommunityPostUserCommentCommand, CommunityPostUserCommentResponse>(unitOfWork)
{
    public override async Task<CommunityPostUserCommentResponse> Handle(UpdateCommunityPostUserCommentCommand request, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var commentEntity = await UnitOfWork.CommunityPostUserComments.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityPostUserCommentNotFoundException(request.Id);

        mapper.Map(request, commentEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);
        await cache.UpdateByIdAsync(commentEntity, commentEntity.Id, cancellationToken);


        return mapper.Map<CommunityPostUserCommentResponse>(commentEntity);
    }
}
