using FluentValidation;
using MapsterMapper;
using NetSpace.User.Application.UserPostUserComment.Exceptions;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment.Requests.PartiallyUpdate;

public sealed record PartiallyUpdateUserPostUserCommentRequest : RequestBase<UserPostUserCommentResponse>
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

public sealed class PartiallyUpdateUserPostUserCommentRequestHandler(IUserPostUserCommentRepository userCommentRepository,
                                                                     IMapper mapper,
                                                                     IValidator<PartiallyUpdateUserPostUserCommentRequest> requestValidator) : RequestHandlerBase<PartiallyUpdateUserPostUserCommentRequest, UserPostUserCommentResponse>
{
    public override async Task<UserPostUserCommentResponse> Handle(PartiallyUpdateUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAndThrowAsync(request, cancellationToken);

        var userCommentEntity = await userCommentRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostUserCommentNotFoundException(request.Id);

        userCommentEntity.Body = request.Body!; // because validator check nullable

        await userCommentRepository.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserPostUserCommentResponse>(userCommentEntity);
    }
}
