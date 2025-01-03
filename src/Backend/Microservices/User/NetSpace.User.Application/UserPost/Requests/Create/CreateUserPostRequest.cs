using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Application.UserPost.Extensions;
using NetSpace.User.UseCases.User;
using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.Application.UserPost.Requests.Create;

public sealed record CreateUserPostRequest : RequestBase<UserPostResponse>
{
    public required string Title { get; set; }
    public required string Body { get; set; }

    public required Guid OwnerId { get; set; }
}


public sealed class CreateUserPostRequestHandler(IUserPostRepository userPostRepository, IUserRepository userRepository) : RequestHandlerBase<CreateUserPostRequest, UserPostResponse>
{
    public override async Task<UserPostResponse> Handle(CreateUserPostRequest request, CancellationToken cancellationToken)
    {

        var userEntity = await userRepository.FindByIdAsync(request.OwnerId, cancellationToken)
            ?? throw new UserNotFoundException(request.OwnerId);

        var entity = request.ToEntity();

        await userPostRepository.AddAsync(entity, cancellationToken);
        await userPostRepository.SaveChangesAsync(cancellationToken);

        return entity.ToResponse();
    }
}
