using KitchenRoutingSystem.Contracts.Orders;
using KitchenRoutingSystem.Domain;
using KitchenRoutingSystem.Infrastructure;
using KitchenRoutingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KitchenRoutingSystem.Controllers;

[ApiController]
[Route("orders")]
public class OrdersController : ControllerBase
{
    private readonly IKitchenRouterService _routerService;
    private readonly KitchenQueueStore _queueStore;

   
    public OrdersController(
        IKitchenRouterService routerService,
        KitchenQueueStore queueStore)
    {
        _routerService = routerService;
        _queueStore = queueStore;
    }

    
    // POST /orders
    
    [HttpPost]
    public async Task<ActionResult<CreateOrderResponse>> CreateOrder([FromBody] CreateOrderRequest request)
    {
        var orderId = Guid.NewGuid();

        var items = request.Items
            .Select(i => new OrderItem(i.Name, i.Area))
            .ToList();

        var order = new Order(orderId, items);

        await _routerService.RouteOrderAsync(order);

        /// 202 = order received and queued for processing
        
        return Accepted(new CreateOrderResponse
        {
            OrderId = order.Id,
            ItemsRouted = order.Items.Count
        });
    }


    ///to see the items in the queues
    
    [HttpGet("queues")]
    public IActionResult GetQueuesStatus()
    {
        return Ok(_queueStore.GetQueueSizes());
    }
}

