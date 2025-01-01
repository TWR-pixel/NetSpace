namespace NetSpace.User.Application.UserPost;

public sealed record UserPostRequest : RequestBase<UserPostResponse>
{
    public required string Title { get; set; }
    public required string Body { get; set; }

    public required Guid OwnerId { get; set; }
}
