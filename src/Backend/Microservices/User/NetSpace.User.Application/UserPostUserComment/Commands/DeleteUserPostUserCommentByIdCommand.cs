using MapsterMapper;
using NetSpace.User.Application.UserPostUserComment.Exceptions;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.UserPostUserComment.Commands;

public sealed record DeleteUserPostUserCommentByIdCommand : CommandBase<UserPostUserCommentResponse>
{
    public required int Id { get; set; }
}

public sealed class DeleteUserPostUserCommentCommandHandler(IUnitOfWork unitOfWork,
                                                            IMapper mapper,
                                                            IUserPostUsercommentDistrubutedCacheStorage cache)
    : CommandHandlerBase<DeleteUserPostUserCommentByIdCommand, UserPostUserCommentResponse>(unitOfWork)
{
    public override async Task<UserPostUserCommentResponse> Handle(DeleteUserPostUserCommentByIdCommand request, CancellationToken cancellationToken)
    {
        var userCommentEntity = await UnitOfWork.UserPostUserComments.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostUserCommentNotFoundException(request.Id);

        await UnitOfWork.UserPostUserComments.DeleteAsync(userCommentEntity, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
        await cache.RemoveByIdAsync(userCommentEntity.Id, cancellationToken);

        return mapper.Map<UserPostUserCommentResponse>(userCommentEntity);
    }
}
