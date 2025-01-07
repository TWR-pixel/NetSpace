using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.Common.Exceptions;
using NetSpace.Community.Application.CommunityPost.Exceptions;
using NetSpace.Community.Domain.CommunityPostUserComment;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunityPostUserComment.Commands;

public sealed record CreateCommunityPostUserCommentCommand : CommandBase<CommunityPostUserCommentResponse>
{
    public required string Body { get; set; }

    public required Guid OwnerId { get; set; }
    public required int CommunityPostId { get; set; }
}

public sealed class CreateCommunityPostUserCommentCommandValidator : AbstractValidator<CreateCommunityPostUserCommentCommand>
{
    public CreateCommunityPostUserCommentCommandValidator()
    {
        RuleFor(c => c.Body)
            .NotEmpty()
            .NotNull()
            .MaximumLength(1024);

        RuleFor(c => c.OwnerId)
            .NotNull()
            .NotEmpty();

        RuleFor(c => c.CommunityPostId)
            .NotNull()
            .NotEmpty();
    }
}

public sealed class CreateCommunityPostUserCommentCommandHandler(IUnitOfWork unitOfWork,
                                                                 IMapper mapper,
                                                                 IValidator<CreateCommunityPostUserCommentCommand> commandValidator) : CommandHandlerBase<CreateCommunityPostUserCommentCommand, CommunityPostUserCommentResponse>(unitOfWork)
{
    public override async Task<CommunityPostUserCommentResponse> Handle(CreateCommunityPostUserCommentCommand request, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var ownerEntity = await UnitOfWork.Users.FindByIdAsync(request.OwnerId, cancellationToken)
            ?? throw new UserNotFoundException(request.OwnerId);

        var communiytPostEntity = await UnitOfWork.CommunityPosts.FindByIdAsync(request.CommunityPostId, cancellationToken)
            ?? throw new CommunityPostNotFoundException(request.CommunityPostId);

        var commentEntity = mapper.Map<CommunityPostUserCommentEntity>(request);

        await UnitOfWork.CommunityPostUserComments.AddAsync(commentEntity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CommunityPostUserCommentResponse>(commentEntity);
    }
}
