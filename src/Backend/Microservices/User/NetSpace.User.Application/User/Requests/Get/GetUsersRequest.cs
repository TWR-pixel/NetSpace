using MapsterMapper;
using NetSpace.User.Application.Common.Cache;
using NetSpace.User.UseCases;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Application.User.Requests.Get;

public sealed record GetUsersRequest : RequestBase<IEnumerable<UserResponse>>
{
    public UserFilterOptions Filter { get; set; } = new UserFilterOptions();
    public PaginationOptions Pagination { get; set; } = new PaginationOptions();
    public SortOptions Sort { get; set; } = new SortOptions();
}


public sealed class GetUsersRequestHandler(IUserRepository userRepository, IUserDistributedCacheStorage cache, IMapper mapper) : RequestHandlerBase<GetUsersRequest, IEnumerable<UserResponse>>
{
    public override async Task<IEnumerable<UserResponse>> Handle(GetUsersRequest request, CancellationToken cancellationToken)
    {
        var users = await userRepository.FilterAsync(request.Filter, request.Pagination, request.Sort, cancellationToken);

        return mapper.Map<IEnumerable<UserResponse>>(users);
    }
}
