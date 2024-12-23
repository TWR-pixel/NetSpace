using MassTransit;
using NetSpace.Common.Messages;

namespace NetSpace.SmsSender.PublicApi.Consumers;

public class OrderCreatedConsumer : IConsumer<OrderCreatedRecord>
{
    private readonly ILogger<OrderCreatedConsumer> _logger;

    public OrderCreatedConsumer(ILogger<OrderCreatedConsumer> logger)
    {
        _logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderCreatedRecord> context)
    {
        _logger.LogInformation("Order Created: OrderId: {OrderId}, Customer: {CustomerName}, Amount: {TotalAmount}",
            context.Message.OrderId, context.Message.CustomerName, context.Message.TotalAmount);
        //Здесь обработка сообщения
    }
}
