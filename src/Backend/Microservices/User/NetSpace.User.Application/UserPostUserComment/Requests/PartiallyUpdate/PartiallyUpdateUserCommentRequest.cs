using NetSpace.User.Application.UserPostUserComment.Exceptions;
using NetSpace.User.Application.UserPostUserComment.Extensions;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment.Requests.PartiallyUpdate;

public sealed record PartiallyUpdateUserCommentRequest : RequestBase<UserPostUserCommentResponse>
{
    public required int Id { get; set; }
    public string? Body { get; set; } 
}

public sealed class PartiallyUpdateUserCommentRequestHandler(IUserPostUserCommentRepository userCommentRepository) : RequestHandlerBase<PartiallyUpdateUserCommentRequest, UserPostUserCommentResponse>
{
    public override async Task<UserPostUserCommentResponse> Handle(PartiallyUpdateUserCommentRequest request, CancellationToken cancellationToken)
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
