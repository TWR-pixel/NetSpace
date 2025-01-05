using MapsterMapper;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Application.UserPost.Exceptions;
using NetSpace.User.Domain.UserPostUserComment;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.UserPostUserComment.Commands.Create;

public sealed record CreateUserPostUserCommentRequest : CommandBase<UserPostUserCommentResponse>
{
    public required string Body { get; set; }

    public Guid UserId { get; set; }
    public int UserPostId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}



public sealed class CreateUserPostUserCommentRequestHandler(IUnitOfWork unitOfWork,
                                                            IMapper mapper) : CommandHandlerBase<CreateUserPostUserCommentRequest, UserPostUserCommentResponse>(unitOfWork)
{
    public override async Task<UserPostUserCommentResponse> Handle(CreateUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {

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
