using NetSpace.User.Application.Common;
using NetSpace.User.Application.User;

namespace NetSpace.User.Application.UserPost;

public sealed record UserPostResponse : ResponseBase
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string Body { get; set; }

    public UserResponse? User { get; set; }
    public Guid UserId { get; set; }

}
