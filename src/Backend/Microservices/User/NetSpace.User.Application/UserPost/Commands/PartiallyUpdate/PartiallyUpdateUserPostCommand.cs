using FluentValidation;
using MapsterMapper;
using NetSpace.User.Application.UserPost.Exceptions;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.UserPost.Commands.PartiallyUpdate;

public sealed record PartiallyUpdateUserPostCommand : CommandBase<UserPostResponse>
{
    public required int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
}

public sealed class PartiallyUpdateUserPostCommandValidator : AbstractValidator<PartiallyUpdateUserPostCommand>
{
    public PartiallyUpdateUserPostCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotNull()
            .NotEmpty()
            .MaximumLength(256)
            .When(c => c.Title is not null);

        RuleFor(c => c.Body)
            .NotNull()
            .NotEmpty()
            .MaximumLength(2048)
            .When(c => c.Title is not null);
    }
}

public sealed class PartiallyUpdateUserPostCommandHandler(IUnitOfWork unitOfWork,
                                                          IValidator<PartiallyUpdateUserPostCommand> requestValidator,
                                                          IMapper mapper) : CommandHandlerBase<PartiallyUpdateUserPostCommand, UserPostResponse>(unitOfWork)
{
    public override async Task<UserPostResponse> Handle(PartiallyUpdateUserPostCommand request, CancellationToken cancellationToken)
    {
        await requestValidator.ValidateAndThrowAsync(request, cancellationToken);

        var userPostEntity = await UnitOfWork.UserPosts.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostNotFoundException(request.Id);

        mapper.Map(request, userPostEntity);

        return mapper.Map<UserPostResponse>(userPostEntity);
    }
}
