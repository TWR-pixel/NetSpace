namespace NetSpace.Common.Messages.Community;

public sealed record CommunityDeletedMessage
{
    public required int Id { get; set; }
}
