using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.CommunityPost.Exceptions;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunityPost.Commands;

public sealed record PartiallyUpdateCommunityPostCommand : CommandBase<CommunityPostResponse>
{
    public required int Id { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }
}

public sealed class PartiallyUpdateCommunityPostCommandValidator : AbstractValidator<PartiallyUpdateCommunityPostCommand>
{
    public PartiallyUpdateCommunityPostCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .NotNull()
            .MaximumLength(256)
            .When(c => c.Title is not null);

        RuleFor(c => c.Body)
            .NotEmpty()
            .NotNull()
            .MaximumLength(12096)
            .When(c => c.Body is not null);

    }
}


public sealed class PartiallyUpdateCommunityPostCommandHandler(IUnitOfWork unitOfWork,
                                                               IMapper mapper,
                                                               IValidator<PartiallyUpdateCommunityPostCommand> commandValidator) : CommandHandlerBase<PartiallyUpdateCommunityPostCommand, CommunityPostResponse>(unitOfWork)
{
    public override async Task<CommunityPostResponse> Handle(PartiallyUpdateCommunityPostCommand request, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var communityPostEntity = await UnitOfWork.CommunityPosts.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new CommunityPostNotFoundException(request.Id);

        mapper.Map(request, communityPostEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CommunityPostResponse>(communityPostEntity);
    }
}
