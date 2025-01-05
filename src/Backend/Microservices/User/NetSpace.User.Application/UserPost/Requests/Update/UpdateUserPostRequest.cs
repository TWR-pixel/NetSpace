using MapsterMapper;
using NetSpace.User.Application.UserPost.Exceptions;
using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.Application.UserPost.Requests.Update;

public sealed record UpdateUserPostRequest : RequestBase<UserPostResponse>
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
}

public sealed class UpdateUserPostRequestHandler(IUserPostRepository userPostRepository, IMapper mapper) : RequestHandlerBase<UpdateUserPostRequest, UserPostResponse>
{
    public override async Task<UserPostResponse> Handle(UpdateUserPostRequest request, CancellationToken cancellationToken)
    {
        var userPostEntity = await userPostRepository.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostNotFoundException(request.Id);

        userPostEntity.Title = request.Title;
        userPostEntity.Body = request.Body;

        await userPostRepository.UpdateAsync(userPostEntity, cancellationToken);
        await userPostRepository.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserPostResponse>(userPostEntity);
    }
}
