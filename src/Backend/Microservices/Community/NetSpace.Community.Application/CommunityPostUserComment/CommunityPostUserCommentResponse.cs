using NetSpace.Community.Application.CommunityPost;
using NetSpace.Community.Application.User;
using NetSpace.Community.Domain.CommunityPost;

namespace NetSpace.Community.Application.CommunityPostUserComment;

public sealed class CommunityPostUserCommentResponse
{
    public int Id { get; set; }
    public required string Body { get; set; }

    public UserResponse? Owner { get; set; }
    public Guid OwnerId { get; set; }

    public CommunityPostResponse? CommunityPost { get; set; }
    public int CommunityPostId { get; set; }
}
