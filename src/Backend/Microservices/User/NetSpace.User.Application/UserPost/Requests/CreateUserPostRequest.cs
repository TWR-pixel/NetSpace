using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.Application.UserPost.Requests;


public sealed class CreateUserPostRequestHandler(IUserPostRepository userPostRepository) : RequestHandlerBase<UserPostRequest, UserPostResponse>
{
    public override Task<UserPostResponse> Handle(UserPostRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
 