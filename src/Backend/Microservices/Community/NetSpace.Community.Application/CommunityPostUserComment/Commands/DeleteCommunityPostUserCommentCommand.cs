using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.CommunityPostUserComment.Caching;
using NetSpace.Community.Application.CommunityPostUserComment.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunityPostUserComment.Commands;

public sealed record DeleteCommunityPostUserCommentCommand : CommandBase<CommunityPostUserCommentResponse>
{
    public required int Id { get; set; }
}

public sealed class DeleteCommunityPostUserCommentCommandValidator : AbstractValidator<DeleteCommunityPostUserCommentCommand>
{
    public DeleteCommunityPostUserCommentCommandValidator()
    {

    }
}

public sealed class DeleteCommunityPostUserCommentCommandHandler(IUnitOfWork unitOfWork,
                                                                 ICommunityPostUserCommentDistributedCache cache,
                                                                 IMapper mapper,
                                                                 IValidator<DeleteCommunityPostUserCommentCommand> commandValidator) : CommandHandlerBase<DeleteCommunityPostUserCommentCommand, CommunityPostUserCommentResponse>(unitOfWork)
{
    public override async Task<CommunityPostUserCommentResponse> Handle(DeleteCommunityPostUserCommentCommand request, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var commentEntity = await UnitOfWork.CommunityPostUserComments.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityPostUserCommentNotFoundException(request.Id);

        await UnitOfWork.CommunityPostUserComments.DeleteAsync(commentEntity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        await cache.RemoveByIdAsync(request.Id, cancellationToken);

        return mapper.Map<CommunityPostUserCommentResponse>(commentEntity);
    }
}
