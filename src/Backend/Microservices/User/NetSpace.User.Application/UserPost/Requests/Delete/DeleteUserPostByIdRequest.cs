using MapsterMapper;
using NetSpace.User.Application.UserPost.Exceptions;
using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.Application.UserPost.Requests.Delete;

public sealed record DeleteUserPostByIdRequest : RequestBase<UserPostResponse>
{
    public required int Id { get; set; }
}

public sealed class DeleteUserPostByIdRequestHandler(IUserPostRepository userPostRepository, IMapper mapper) : RequestHandlerBase<DeleteUserPostByIdRequest, UserPostResponse>
{
    public override async Task<UserPostResponse> Handle(DeleteUserPostByIdRequest request, CancellationToken cancellationToken)
    {
        var userPost = await userPostRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostNotFoundException(request.Id);

        await userPostRepository.DeleteAsync(userPost, cancellationToken);
        await userPostRepository.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserPostResponse>(userPost);
    }
}
