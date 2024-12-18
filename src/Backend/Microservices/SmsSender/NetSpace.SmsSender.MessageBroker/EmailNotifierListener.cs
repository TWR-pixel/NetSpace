using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text.Json;

namespace NetSpace.SmsSender.PublicApi;

public sealed class EmailNotifierListener(IConnectionFactory connectionFactory) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var connection = await connectionFactory.CreateConnectionAsync(stoppingToken);
        var channel = await connection.CreateChannelAsync(cancellationToken: stoppingToken);
        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (sender, args) =>
        {
            var emailMessage = JsonSerializer.Deserialize<EmailMessage>(args.Body.ToArray());
            Console.WriteLine("Recieved email message: {0}", emailMessage);

            await channel.BasicAckAsync(args.DeliveryTag, false);
        };

        await channel.BasicConsumeAsync("email_notifier", false, consumer, cancellationToken: stoppingToken);
    }
}