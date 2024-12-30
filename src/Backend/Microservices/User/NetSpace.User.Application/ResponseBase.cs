namespace NetSpace.User.Application;

public abstract record ResponseBase
{
    public string Status { get; set; } = "Success";
}
