using FluentValidation;
using MapsterMapper;
using NetSpace.Community.Application.Community.Exceptions;
using NetSpace.Community.Application.CommunityPost.Caching;
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

    }
}

public sealed class CreateCommunityPostCommandHandler(IUnitOfWork unitOfWork,
                                                      ICommunityPostDistributedCache cache,
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

        await cache.AddAsync(communityPostEntity, cancellationToken);

        return mapper.Map<CommunityPostResponse>(communityPostEntity);
    }
}
