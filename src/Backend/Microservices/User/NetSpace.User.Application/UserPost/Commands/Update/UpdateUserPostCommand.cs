using MapsterMapper;
using NetSpace.User.Application.UserPost.Exceptions;
using NetSpace.User.UseCases.Common;

namespace NetSpace.User.Application.UserPost.Commands.Update;

public sealed record UpdateUserPostCommand : CommandBase<UserPostResponse>
{
    public required int Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }
}

public sealed class UpdateUserPostCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    : CommandHandlerBase<UpdateUserPostCommand, UserPostResponse>(unitOfWork)
{
    public override async Task<UserPostResponse> Handle(UpdateUserPostCommand request, CancellationToken cancellationToken)
    {
        var userPostEntity = await UnitOfWork.UserPosts.FindByIdAsync(request.Id, cancellationToken)
            ?? throw new UserPostNotFoundException(request.Id);

        mapper.Map(request, userPostEntity);

        await UnitOfWork.SaveChangesAsync(cancellationToken);

        return mapper.Map<UserPostResponse>(userPostEntity);
    }
}
