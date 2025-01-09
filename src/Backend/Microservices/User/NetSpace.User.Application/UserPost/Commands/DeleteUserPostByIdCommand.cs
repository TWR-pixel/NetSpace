using MapsterMapper;
using NetSpace.User.Application.UserPost.Exceptions;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.UserPost.Commands;

public sealed record DeleteUserPostByIdCommand : CommandBase<UserPostResponse>
{
    public required int Id { get; set; }
}

public sealed class DeleteUserPostByIdCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IUserPostDistributedCacheStorage cache)
    : CommandHandlerBase<DeleteUserPostByIdCommand, UserPostResponse>(unitOfWork)
{
    public override async Task<UserPostResponse> Handle(DeleteUserPostByIdCommand request, CancellationToken cancellationToken)
    {
        var userPost = await UnitOfWork.UserPosts.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostNotFoundException(request.Id);

        await UnitOfWork.UserPosts.DeleteAsync(userPost, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
        await cache.RemoveByIdAsync(userPost.Id, cancellationToken);

        return mapper.Map<UserPostResponse>(userPost);
    }
}
