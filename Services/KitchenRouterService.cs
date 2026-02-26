using KitchenRoutingSystem.Domain;
using KitchenRoutingSystem.Infrastructure;
using KitchenRoutingSystem.Services.Interfaces;

namespace KitchenRoutingSystem.Services;

/// service responsible for routing order items to the correct kitchen queues.

public class KitchenRouterService : IKitchenRouterService
{
    private readonly KitchenQueueStore _queueStore;

    public KitchenRouterService(KitchenQueueStore queueStore)
    {
        _queueStore = queueStore;
    }

    public Task RouteOrderAsync(Order order)
    {
        if (order is null)
            throw new ArgumentNullException(nameof(order));

        var items = order.Items ?? Array.Empty<OrderItem>();

        foreach (var item in items)
        {
            var queue = _queueStore.GetQueue(item.Area);
            queue.Enqueue(item);
        }

        return Task.CompletedTask;
    }
}
                                                                                                                                                                                                                            