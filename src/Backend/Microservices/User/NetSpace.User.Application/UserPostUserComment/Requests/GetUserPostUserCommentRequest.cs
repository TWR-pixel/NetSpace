
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment.Requests;

public sealed record GetUserPostUserCommentRequest : RequestBase<IEnumerable<UserPostUserCommentResponse>>
{
    public UserPostUserCommentFilterOptions Filter { get; set; } = new();
}

public sealed class GetUserPostUserCommentRequestHandler(IUserPostUserCommentRepository userCommentRepository) : RequestHandlerBase<GetUserPostUserCommentRequest, IEnumerable<UserPostUserCommentResponse>>
{
    public override Task<IEnumerable<UserPostUserCommentResponse>> Handle(GetUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
