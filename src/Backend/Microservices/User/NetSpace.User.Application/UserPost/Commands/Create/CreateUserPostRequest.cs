using FluentValidation;
using MapsterMapper;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Domain.UserPost;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.UserPost.Commands.Create;

public sealed record CreateUserPostRequest : CommandBase<UserPostResponse>
{
    public required string Title { get; set; }
    public required string Body { get; set; }

    public required Guid OwnerId { get; set; }
}

public sealed class CreateUserPostRequestValidator : AbstractValidator<CreateUserPostRequest>
{
    public CreateUserPostRequestValidator()
    {

    }
}

public sealed class CreateUserPostRequestHandler(IUnitOfWork unitOfWork,
                                                 IMapper mapper,
                                                 IValidator<CreateUserPostRequest> requestValidator) : CommandHandlerBase<CreateUserPostRequest, UserPostResponse>(unitOfWork)
{
    public override async Task<UserPostResponse> Handle(CreateUserPostRequest request, CancellationToken cancellationToken)
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
