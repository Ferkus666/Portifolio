namespace KitchenRoutingSystem.Domain;

///represents an individual item of the order


public class OrderItem
{
    public string Name { get; }
    public KitchenArea Area { get; }

    public OrderItem(string name, KitchenArea area)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Item name is required.", nameof(name));

        Name = name;
        Area = area;
    }
}
