using FluentValidation;
using MapsterMapper;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Application.UserPost.Exceptions;
using NetSpace.User.Domain.UserPostUserComment;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.UserPostUserComment.Commands.Create;

public sealed record CreateUserPostUserCommentCommand : CommandBase<UserPostUserCommentResponse>
{
    public required string Body { get; set; }

    public required Guid UserId { get; set; }
    public required int UserPostId { get; set; }
}

public sealed class CreateUserPostUserCommentCommandValidator : AbstractValidator<CreateUserPostUserCommentCommand>
{
    public CreateUserPostUserCommentCommandValidator()
    {
        RuleFor(u => u.Body)
            .NotNull()
            .NotEmpty()
            .MaximumLength(512);
    }
}

public sealed class CreateUserPostUserCommentCommandHandler(IUnitOfWork unitOfWork,
                                                            IMapper mapper,
                                                            IValidator<CreateUserPostUserCommentCommand> commandValidator) : CommandHandlerBase<CreateUserPostUserCommentCommand, UserPostUserCommentResponse>(unitOfWork)
{
    public override async Task<UserPostUserCommentResponse> Handle(CreateUserPostUserCommentCommand request, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        _ = await UnitOfWork.Users.FindByIdAsync(request.UserId, cancellationToken)
            ?? throw new UserNotFoundException(request.UserId);

        _ = await UnitOfWork.UserPosts.FindByIdAsync(request.UserPostId, cancellationToken)
            ?? throw new UserPostNotFoundException(request.UserPostId);

        var userCommentEntity = mapper.Map<UserPostUserCommentEntity>(request);

        await UnitOfWork.UserPostUserComments.AddAsync(userCommentEntity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserPostUserCommentResponse>(userCommentEntity);
    }
}
