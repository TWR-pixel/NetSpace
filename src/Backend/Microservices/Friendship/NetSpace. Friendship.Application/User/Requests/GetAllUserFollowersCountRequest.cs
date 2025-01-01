using NetSpace.Friendship.UseCases;

namespace NetSpace.Friendship.Application.User.Requests;

public sealed record GetAllUserFollowersCountRequest : RequestBase<GetAllUserFollowersCountResponse>
{
    public required Guid Id { get; set; }
}

public sealed record GetAllUserFollowersCountResponse : ResponseBase;

public sealed class GetAllUserFollowersCountRequestHandler(IUserRepository userRepository) : RequestHandlerBase<GetAllUserFollowersCountRequest, GetAllUserFollowersCountResponse>
{
    public override async Task<GetAllUserFollowersCountResponse> Handle(GetAllUserFollowersCountRequest request, CancellationToken cancellationToken)
    {
        var result = await userRepository.FollowersCountById(request.Id, cancellationToken);

        throw new NotImplementedException();
    }
}
