using NetSpace.User.Application.UserPostUserComment.Exceptions;
using NetSpace.User.Application.UserPostUserComment.Extensions;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment.Requests.Delete;

public sealed record DeleteUserPostUserCommentRequest : RequestBase<UserPostUserCommentResponse>
{
    public required int Id { get; set; }
}

public sealed class DeleteUserPostUserCommentRequestHandler(IUserPostUserCommentRepository userCommentRepository) : RequestHandlerBase<DeleteUserPostUserCommentRequest, UserPostUserCommentResponse>
{
    public override async Task<UserPostUserCommentResponse> Handle(DeleteUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        var userCommentEntity = await userCommentRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostUserCommentNotFoundException(request.Id);

        await userCommentRepository.DeleteAsync(userCommentEntity, cancellationToken);
        await userCommentRepository.SaveChangesAsync(cancellationToken);

        return userCommentEntity.ToResponse();
    }
}
