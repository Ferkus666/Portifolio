using System.ComponentModel.DataAnnotations;
using KitchenRoutingSystem.Domain;

namespace KitchenRoutingSystem.Contracts.Orders;

public class CreateOrderRequest
{
    [Required(ErrorMessage = "require an item")]
    [MinLength(1, ErrorMessage = "The order must contain at least 1 item.")]
    public List<CreateOrderItemRequest> Items { get; set; } = new();
}

public class CreateOrderItemRequest
{
    [Required(ErrorMessage = "Enter a Name")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Enter an Area")]
    public KitchenArea Area { get; set; }
}
