using KitchenRoutingSystem.Domain;

namespace KitchenRoutingSystem.Services.Interfaces;


/// interface of the routing service

public interface IKitchenRouterService
{
    Task RouteOrderAsync(Order order);
}
