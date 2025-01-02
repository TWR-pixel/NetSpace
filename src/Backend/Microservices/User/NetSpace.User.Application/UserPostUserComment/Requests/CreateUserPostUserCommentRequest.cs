using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Application.UserPost.Exceptions;
using NetSpace.User.Application.UserPostUserComment.Extensions;
using NetSpace.User.UseCases.User;
using NetSpace.User.UseCases.UserPost;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment.Requests;

public sealed record CreateUserPostUserCommentRequest : RequestBase<UserPostUserCommentResponse>
{
    public required UserPostUserCommentRequest UserCommentRequest { get; set; }
}
public sealed class CreateUserPostUserCommentRequestHandler(IUserPostUserCommentRepository userCommentRepository,
                                                            IUserRepository userRepository,
                                                            IUserPostRepository userPostRepository) : RequestHandlerBase<CreateUserPostUserCommentRequest, UserPostUserCommentResponse>
{
    public override async Task<UserPostUserCommentResponse> Handle(CreateUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        var userComment = request.UserCommentRequest;

        var owner = await userRepository.FindByIdAsync(userComment.UserId, cancellationToken)
            ?? throw new UserNotFoundException(userComment.UserId);

        var userPost = await userPostRepository.FindByIdAsync(userComment.UserPostId, cancellationToken)
            ?? throw new UserPostNotFoundException(userComment.UserPostId);

        var userCommentEntity = userComment.ToEntity(owner, userPost);

        await userCommentRepository.AddAsync(userCommentEntity, cancellationToken);
        await userCommentRepository.SaveChangesAsync(cancellationToken);

        return userCommentEntity.ToResponse();
    }
}
