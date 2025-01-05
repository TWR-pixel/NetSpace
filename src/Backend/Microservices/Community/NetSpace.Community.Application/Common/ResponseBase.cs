namespace NetSpace.Community.Application.Common;

public abstract record ResponseBase
{
    public string Status { get; set; } = "Success";
}
