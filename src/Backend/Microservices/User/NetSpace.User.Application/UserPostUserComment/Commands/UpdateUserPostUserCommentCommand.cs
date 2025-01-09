using FluentValidation;
using MapsterMapper;
using NetSpace.User.Application.UserPostUserComment.Exceptions;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.UserPostUserComment.Commands;

public sealed record UpdateUserPostUserCommentCommand : CommandBase<UserPostUserCommentResponse>
{
    public required int Id { get; set; }
    public required string Body { get; set; }
}

public sealed class UpdateUserPostUserCommentCommandValidator : AbstractValidator<UpdateUserPostUserCommentCommand>
{
    public UpdateUserPostUserCommentCommandValidator()
    {
        RuleFor(u => u.Body)
            .NotNull()
            .NotEmpty()
            .MaximumLength(512);
    }
}

public sealed class UpdateUserPostUserCommentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<UpdateUserPostUserCommentCommand> commandValidator)
    : CommandHandlerBase<UpdateUserPostUserCommentCommand, UserPostUserCommentResponse>(unitOfWork)
{
    public override async Task<UserPostUserCommentResponse> Handle(UpdateUserPostUserCommentCommand request, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var userCommentEntity = await UnitOfWork.UserPostUserComments.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostUserCommentNotFoundException(request.Id);

        mapper.Map(request, userCommentEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserPostUserCommentResponse>(userCommentEntity);
    }
}
