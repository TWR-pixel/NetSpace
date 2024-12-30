namespace NetSpace.Friendship.Application;

public abstract record ResponseBase
{
    public string Status { get; set; } = "Success";
}
