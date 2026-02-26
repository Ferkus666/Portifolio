namespace KitchenRoutingSystem.Contracts.Orders;

public class CreateOrderResponse
{
    public Guid OrderId { get; set; }
    public int ItemsRouted { get; set; }
}
