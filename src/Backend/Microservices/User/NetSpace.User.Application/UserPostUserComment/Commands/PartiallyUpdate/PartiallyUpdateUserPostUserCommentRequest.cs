using FluentValidation;
using MapsterMapper;
using NetSpace.User.Application.UserPostUserComment.Exceptions;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.UserPostUserComment.Commands.PartiallyUpdate;

public sealed record PartiallyUpdateUserPostUserCommentRequest : CommandBase<UserPostUserCommentResponse>
{
    public required int Id { get; set; }
    public string? Body { get; set; }
}

public sealed class PartiallyUpdateUserPostUserCommentRequestValidator : AbstractValidator<PartiallyUpdateUserPostUserCommentRequest>
{
    public PartiallyUpdateUserPostUserCommentRequestValidator()
    {
        RuleFor(p => p.Body)
            .NotEmpty()
            .WithMessage("Body must be not empty.")
            .MinimumLength(1)
            .MaximumLength(2048)
            .WithMessage("Maximum comment length is 2048")
            .When(p => p.Body is not null);
    }
}

public sealed class PartiallyUpdateUserPostUserCommentRequestHandler(IUnitOfWork unitOfWork,
                                                                     IMapper mapper,
                                                                     IValidator<PartiallyUpdateUserPostUserCommentRequest> requestValidator)
    : CommandHandlerBase<PartiallyUpdateUserPostUserCommentRequest, UserPostUserCommentResponse>(unitOfWork)
{
    public override async Task<UserPostUserCommentResponse> Handle(PartiallyUpdateUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAndThrowAsync(request, cancellationToken);

        var userCommentEntity = await UnitOfWork.UserPostUserComments.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostUserCommentNotFoundException(request.Id);

        mapper.Map(request, userCommentEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserPostUserCommentResponse>(userCommentEntity);
    }
}
