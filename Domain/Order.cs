namespace KitchenRoutingSystem.Domain;

public class Order
{
    public Guid Id { get; }
    public IReadOnlyCollection<OrderItem> Items { get; }

    public Order(Guid id, List<OrderItem> items)
    {
        if (id == Guid.Empty)
            throw new ArgumentException("Order id is invalid", nameof(id));

        Items = items ?? throw new ArgumentNullException(nameof(items));

        if (!Items.Any())
            throw new ArgumentException("Order must have at least one item", nameof(items));

        Id = id;
    }
}

