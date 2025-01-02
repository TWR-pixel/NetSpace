using NetSpace.User.UseCases;
using NetSpace.User.UseCases.UserPost;

namespace NetSpace.User.Application.UserPost.Requests;

public sealed record GetUserPostRequest : RequestBase<IEnumerable<UserPostResponse>>
{
    public UserPostFilterOptions Filter { get; set; } = new();
    public PaginationOptions Pagination { get; set; } = new();
    public SortOptions Sort { get; set; } = new();
}
