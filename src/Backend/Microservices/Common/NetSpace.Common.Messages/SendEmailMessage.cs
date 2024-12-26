namespace NetSpace.Common.Messages;

public sealed record SendEmailMessage
{    public required string To { get; set; }
    public required string Body { get; set; }
}