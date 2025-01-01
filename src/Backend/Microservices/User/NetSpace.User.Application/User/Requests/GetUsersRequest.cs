using NetSpace.User.Application.Common.Cache;
using NetSpace.User.Application.User.Exceptions;
using NetSpace.User.Application.User.Extensions;
using NetSpace.User.UseCases;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Application.User.Requests;

public sealed record GetUsersRequest : RequestBase<IEnumerable<UserResponse>>
{
    public Guid Id { get; set; }
    public UserFilterOptions FilterOptions { get; set; } = new UserFilterOptions();
    public PaginationOptions PaginationOptions { get; set; } = new PaginationOptions();

}


public sealed class GetUsersRequestHandler(IUserRepository userRepository, IUserDistributedCacheStorage cache) : RequestHandlerBase<GetUsersRequest, IEnumerable<UserResponse>>
{
    public override async Task<IEnumerable<UserResponse>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await userRepository.FilterAsync(request.FilterOptions, request.PaginationOptions, cancellationToken)
            ?? throw new UserNotFoundException(request.Id);

        return users.ToResponses();
    }
}
