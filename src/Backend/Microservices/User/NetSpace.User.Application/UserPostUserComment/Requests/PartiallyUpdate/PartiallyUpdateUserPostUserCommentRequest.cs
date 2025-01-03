using NetSpace.User.Application.UserPostUserComment.Exceptions;
using NetSpace.User.Application.UserPostUserComment.Extensions;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment.Requests.PartiallyUpdate;

public sealed record PartiallyUpdateUserPostUserCommentRequest : RequestBase<UserPostUserCommentResponse>
{
    public required int Id { get; set; }
    public string? Body { get; set; } 
}

public sealed class PartiallyUpdateUserPostUserCommentRequestHandler(IUserPostUserCommentRepository userCommentRepository) : RequestHandlerBase<PartiallyUpdateUserPostUserCommentRequest, UserPostUserCommentResponse>
{
    public override async Task<UserPostUserCommentResponse> Handle(PartiallyUpdateUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        var userCommentEntity = await userCommentRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostUserCommentNotFoundException(request.Id);

        if (!string.IsNullOrWhiteSpace(request.Body))
            userCommentEntity.Body = request.Body;

        await userCommentRepository.UpdateAsync(userCommentEntity, cancellationToken);
        await userCommentRepository.SaveChangesAsync(cancellationToken);

        return userCommentEntity.ToResponse();
    }
}
