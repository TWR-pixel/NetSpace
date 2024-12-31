namespace NetSpace.Identity.Application;

public abstract record ResponseBase
{
    public string Status { get; set; } = "Success";
}
