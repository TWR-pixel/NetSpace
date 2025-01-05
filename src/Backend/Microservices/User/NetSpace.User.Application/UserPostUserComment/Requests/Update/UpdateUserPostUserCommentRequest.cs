using MapsterMapper;
using NetSpace.User.Application.UserPostUserComment.Exceptions;
using NetSpace.User.UseCases.UserPostUserComment;

namespace NetSpace.User.Application.UserPostUserComment.Requests.Update;

public sealed record UpdateUserPostUserCommentRequest : RequestBase<UserPostUserCommentResponse>
{
    public required int Id { get; set; }
    public required string Body { get; set; }
}

public sealed class UpdateUserPostUserCommentRequestHandler(IUserPostUserCommentRepository userCommentRepository, IMapper mapper) : RequestHandlerBase<UpdateUserPostUserCommentRequest, UserPostUserCommentResponse>
{
    public override async Task<UserPostUserCommentResponse> Handle(UpdateUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        var userCommentEntity = await userCommentRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostUserCommentNotFoundException(request.Id);

        userCommentEntity.Body = request.Body;

        await userCommentRepository.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserPostUserCommentResponse>(userCommentEntity);
    }
}
