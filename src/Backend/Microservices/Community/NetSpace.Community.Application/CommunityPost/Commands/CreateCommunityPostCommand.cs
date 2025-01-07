using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.Domain.CommunityPost;
using NetSpace.Community.UseCases.Common;

namespace NetSpace.Community.Application.CommunityPost.Commands;

public sealed record CreateCommunityPostCommand : CommandBase<CommunityPostResponse>
{
    public required string Title { get; set; }
    public required string Body { get; set; }

    public required int CommunityId { get; set; }
}

public sealed class CreateCommunityPostCommandValidator : AbstractValidator<CreateCommunityPostCommand>
{
    public CreateCommunityPostCommandValidator()
    {
        RuleFor(c => c.Title)
            .NotEmpty()
            .NotNull()
            .MaximumLength(256);

        RuleFor(c => c.Body)
            .NotEmpty()
            .NotNull()
            .MaximumLength(12096);

        RuleFor(c => c.CommunityId)
            .NotEmpty()
            .NotNull();
    }
}

public sealed class CreateCommunityPostCommandHandler(IUnitOfWork unitOfWork,
                                                      IMapper mapper,
                                                      IValidator<CreateCommunityPostCommand> commandValidator) : CommandHandlerBase<CreateCommunityPostCommand, CommunityPostResponse>(unitOfWork)
{
    public override async Task<CommunityPostResponse> Handle(CreateCommunityPostCommand request, CancellationToken cancellationToken)
    {
        await commandValidator.ValidateAndThrowAsync(request, cancellationToken);

        var communityEntity = await UnitOfWork.Communities.FindByIdAsync(request.CommunityId, cancellationToken)
            ?? throw new CommunityNotFoundException(request.CommunityId);

        var communityPostEntity = mapper.Map<CommunityPostEntity>(request);

        await UnitOfWork.CommunityPosts.AddAsync(communityPostEntity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<CommunityPostResponse>(communityPostEntity);
    }
}
