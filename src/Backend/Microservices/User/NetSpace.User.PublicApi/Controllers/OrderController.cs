using MassTransit;
using Microsoft.AspNetCore.Mvc;
using NetSpace.Common.Messages;

namespace NetSpace.User.PublicApi.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndpoint;

    public OrderController(IPublishEndpoint publishEndpoint)
    {
        _publishEndpoint = publishEndpoint;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderCreatedRecord order)
    {
        await _publishEndpoint.Publish(order);
        return Ok(order);
    }
}
