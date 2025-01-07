using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.CommunityPost.Exceptions;
using NetSpace.Community.Application.CommunityPostUserComment.Caching;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunityPostUserComment.Commands;

public sealed record PartiallyUpdateCommunityPostUserCommentCommand : CommandBase<CommunityPostUserCommentResponse>
{
    public required int Id { get; set; }
    public string? Body { get; set; }
}

public sealed class PartiallyUpdateCommunityPostUserCommentCommandValidator : AbstractValidator<PartiallyUpdateCommunityPostUserCommentCommand>
{
    public PartiallyUpdateCommunityPostUserCommentCommandValidator()
    {
        RuleFor(c => c.Body)
            .NotEmpty()
            .NotNull()
            .MaximumLength(1024)
            .When(c => c.Body is not null);
    }
}


public sealed class PartiallyUpdateCommunityPostUserCommentCommandHandler(IUnitOfWork unitOfWork,
                                                                          ICommunityPostUserCommentDistributedCache cache,
                                                                          IMapper mapper,
                                                                          IValidator<PartiallyUpdateCommunityPostUserCommentCommand> commandValidatork)
    : CommandHandlerBase<PartiallyUpdateCommunityPostUserCommentCommand, CommunityPostUserCommentResponse>(unitOfWork)
{
    public override async Task<CommunityPostUserCommentResponse> Handle(PartiallyUpdateCommunityPostUserCommentCommand request, CancellationToken cancellationToken)
    {
        await commandValidatork.ValidateAndThrowAsync(request, cancellationToken);

        var commentEntity = await UnitOfWork.CommunityPostUserComments.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityPostNotFoundException(request.Id);

        mapper.Map(request, commentEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);
        await cache.UpdateByIdAsync(commentEntity, commentEntity.Id, cancellationToken);

        return mapper.Map<CommunityPostUserCommentResponse>(commentEntity);
    }
}
