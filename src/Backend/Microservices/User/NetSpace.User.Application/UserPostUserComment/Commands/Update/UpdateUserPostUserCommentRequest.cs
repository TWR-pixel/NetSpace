using MapsterMapper;
using NetSpace.User.Application.UserPostUserComment.Exceptions;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.UserPostUserComment.Commands.Update;

public sealed record UpdateUserPostUserCommentRequest : CommandBase<UserPostUserCommentResponse>
{
    public required int Id { get; set; }
    public required string Body { get; set; }
}

public sealed class UpdateUserPostUserCommentRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) 
    : CommandHandlerBase<UpdateUserPostUserCommentRequest, UserPostUserCommentResponse>(unitOfWork)
{
    public override async Task<UserPostUserCommentResponse> Handle(UpdateUserPostUserCommentRequest request, CancellationToken cancellationToken)
    {
        var userCommentEntity = await UnitOfWork.UserPostUserComments.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostUserCommentNotFoundException(request.Id);

        mapper.Map(request, userCommentEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserPostUserCommentResponse>(userCommentEntity);
    }
}
