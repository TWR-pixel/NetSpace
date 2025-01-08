using MapsterMapper;
using NetSpace.User.UseCases.Common;
using NetSpace.User.UseCases.User;

namespace NetSpace.User.Application.User.Queries.Get;

public sealed record GetUsersQuery : QueryBase<IEnumerable<UserResponse>>
{
    public UserFilterOptions Filter { get; set; } = new UserFilterOptions();
    public PaginationOptions Pagination { get; set; } = new PaginationOptions();
    public SortOptions Sort { get; set; } = new SortOptions();
}


public sealed class GetUsersQueryHandler(IReadonlyUnitOfWork unitOfWork, IMapper mapper) : QueryHandlerBase<GetUsersQuery, IEnumerable<UserResponse>>(unitOfWork)
{
    public override async Task<IEnumerable<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await UnitOfWork.Users.FilterAsync(request.Filter, request.Pagination, request.Sort, cancellationToken);

        return mapper.Map<IEnumerable<UserResponse>>(users);
    }
}
