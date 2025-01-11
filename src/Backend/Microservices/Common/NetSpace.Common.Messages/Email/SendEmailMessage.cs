namespace NetSpace.Common.Messages.Email;

public sealed record SendEmailMessage
{
    public required string To { get; set; }
    public required string Subject { get; set; }
    public required string Body { get; set; }
}