using MapsterMapper;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Application.UserPost.Exceptions;
using NetSpace.User.Domain.UserPostUserComment;
using NetSpace.User.UseCases.User;
using NetSpace.User.UseCases.UserPost;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment.Requests.Create;

public sealed record CreateUserPostUserCommentRequest : RequestBase<UserPostUserCommentResponse>
{
    public required string Body { get; set; }

    public Guid UserId { get; set; }
    public int UserPostId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}



public sealed class CreateUserPostUserCommentRequestHandler(IUserPostUserCommentRepository userCommentRepository,
                                                            IUserRepository userRepository,
                                                            IUserPostRepository userPostRepository,
                                                            IMapper mapper) : RequestHandlerBase<CreateUserPostUserCommentRequest, UserPostUserCommentResponse>
{
    public override async Task<UserPostUserCommentResponse> Handle(CreateUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {

        _ = await userRepository.FindByIdAsync(request.UserId, cancellationToken)
            ?? throw new UserNotFoundException(request.UserId);

        _ = await userPostRepository.FindByIdAsync(request.UserPostId, cancellationToken)
            ?? throw new UserPostNotFoundException(request.UserPostId);

        var userCommentEntity = mapper.Map<UserPostUserCommentEntity>(request);

        await userCommentRepository.AddAsync(userCommentEntity, cancellationToken);
        await userCommentRepository.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserPostUserCommentResponse>(userCommentEntity);
    }
}
