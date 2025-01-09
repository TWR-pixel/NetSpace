using FluentValidation;
using MapsterMapper;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Domain.UserPost;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.UserPost.Commands;

public sealed record CreateUserPostCommand : CommandBase<UserPostResponse>
{
    public required string Title { get; set; }
    public required string Body { get; set; }

    public required Guid OwnerId { get; set; }
}

public sealed class CreateUserPostCommandValidator : AbstractValidator<CreateUserPostCommand>
{
    public CreateUserPostCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(256);

        RuleFor(c => c.Body)
            .NotNull()
            .NotEmpty()
            .MaximumLength(2048);

        RuleFor(c => c.OwnerId)
            .NotNull()
            .NotEmpty();
    }
}

public sealed class CreateUserPostCommandHandler(IUnitOfWork unitOfWork,
                                                 IMapper mapper,
                                                 IValidator<CreateUserPostCommand> requestValidator) : CommandHandlerBase<CreateUserPostCommand, UserPostResponse>(unitOfWork)
{
    public override async Task<UserPostResponse> Handle(CreateUserPostCommand request, CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAndThrowAsync(request, cancellationToken);

        _ = await UnitOfWork.Users.FindByIdAsync(request.OwnerId, cancellationToken)
            ?? throw new UserNotFoundException(request.OwnerId);

        var entity = mapper.Map<UserPostEntity>(request);

        await UnitOfWork.UserPosts.AddAsync(entity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserPostResponse>(entity);
    }
}
