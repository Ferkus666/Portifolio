using System.Collections.Concurrent;
using System.Linq;
using KitchenRoutingSystem.Domain;

namespace KitchenRoutingSystem.Infrastructure;

public class KitchenQueueStore
{
    private readonly ConcurrentDictionary<KitchenArea, ConcurrentQueue<OrderItem>> _queues = new();

    public ConcurrentQueue<OrderItem> GetQueue(KitchenArea area)
    {
        return _queues.GetOrAdd(area, _ => new ConcurrentQueue<OrderItem>());
    }

    
    public IReadOnlyDictionary<KitchenArea, int> GetQueueSizes()
    {
   
        ///creates a safe snapshot of the queue sizes
       
        return _queues.ToDictionary(
            kvp => kvp.Key,
            kvp => kvp.Value.Count
        );
    }
}
