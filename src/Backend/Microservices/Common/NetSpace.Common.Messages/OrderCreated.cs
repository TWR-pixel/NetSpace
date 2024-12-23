namespace NetSpace.Common.Messages;

public record OrderCreatedRecord(Guid OrderId, string CustomerName, decimal TotalAmount);

