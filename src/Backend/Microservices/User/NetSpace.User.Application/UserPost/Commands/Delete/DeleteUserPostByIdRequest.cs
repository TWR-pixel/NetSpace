using MapsterMapper;
using MassTransit;
using NetSpace.User.Application.UserPost.Exceptions;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.UserPost.Commands.Delete;

public sealed record DeleteUserPostByIdRequest : CommandBase<UserPostResponse>
{
    public required int Id { get; set; }
}

public sealed class DeleteUserPostByIdRequestHandler(IUnitOfWork unitOfWork,IMapper mapper, IPublishEndpoint publisher)
    : CommandHandlerBase<DeleteUserPostByIdRequest, UserPostResponse>(unitOfWork)
{
    public override async Task<UserPostResponse> Handle(DeleteUserPostByIdRequest request, CancellationToken cancellationToken)
    {
        var userPost = await UnitOfWork.UserPosts.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostNotFoundException(request.Id);

        await UnitOfWork.UserPosts.DeleteAsync(userPost, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserPostResponse>(userPost);
    }
}
