using NetSpace.EmailSender.Application;

namespace NetSpace.EmailSender.Tests.Unit.Application.EmailSender.Fakes;

public sealed class FakeEmailSender : IEmailSender
{
    //public Task SendAsync(string from, string to, string subject, string body)
    //{
    //    Console.WriteLine(from + to + subject + body);

    //    return Task.CompletedTask;
    //}
    public Task SendAsync()
    {
        throw new NotImplementedException();
    }
}
