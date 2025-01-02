using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Application.UserPost.Extensions;
using NetSpace.User.UseCases.User;
using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.Application.UserPost.Requests;

public sealed record CreateUserPostRequest : RequestBase<UserPostResponse>
{
    public required UserPostRequest UserPostRequest { get; set; }
}


public sealed class CreateUserPostRequestHandler(IUserPostRepository userPostRepository, IUserRepository userRepository) : RequestHandlerBase<CreateUserPostRequest, UserPostResponse>
{
    public override async Task<UserPostResponse> Handle(CreateUserPostRequest request, CancellationToken cancellationToken)
    {
        var userPost = request.UserPostRequest;

        var userEntity = await userRepository.FindByIdAsync(userPost.OwnerId, cancellationToken)
            ?? throw new UserNotFoundException(userPost.OwnerId);

        var entity = userPost.ToEntity(userEntity);

        await userPostRepository.AddAsync(entity, cancellationToken);
        await userPostRepository.SaveChangesAsync(cancellationToken);

        return entity.ToResponse();
    }
}
