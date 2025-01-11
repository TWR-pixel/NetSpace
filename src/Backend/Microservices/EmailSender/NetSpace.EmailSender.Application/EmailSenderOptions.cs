namespace NetSpace.EmailSender.Application;

public sealed class EmailSenderOptions
{
    public required string From { get; set; }
    public required string Password { get; set; }
    public required string Host { get; set; }
    public required int Port { get; set; }
}
