using NetSpace.Community.Application.Community;
using NetSpace.Community.Application.CommunityPostUserComment;

namespace NetSpace.Community.Application.CommunityPost;

public sealed record CommunityPostResponse : ResponseBase
{
    public int Id { get; set; }

    public required string Title { get; set; }
    public required string Body { get; set; }

    public CommunityResponse? Community { get; set; }
    public int CommunityId { get; set; }
}
