using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.CommunityPostUserComment.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunityPostUserComment.Commands;

public sealed record UpdateCommunityPostUserCommentCommand : CommandBase<CommunityPostUserCommentResponse>
{
    public required int Id { get; set; }
    public required string Body { get; set; }
}

public sealed class UpdateCommunityPostUserCommentCommandValidator : AbstractValidator<UpdateCommunityPostUserCommentCommand>
{
    public UpdateCommunityPostUserCommentCommandValidator()
    {
        RuleFor(c => c.Body)
            .NotEmpty()
            .NotNull()
            .MaximumLength(1024);

    }
}

public sealed class UpdateCommunityPostUserCommentCommandHandler(IUnitOfWork unitOfWork,
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
        
        return mapper.Map<CommunityPostUserCommentResponse>(commentEntity);
    }
}
