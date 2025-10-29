using Cryptocop.Models.Dtos;
using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Interfaces;
using Cryptocop.Services.Interfaces;

namespace Cryptocop.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    private readonly IShoppingCartRepository _shoppingCartRepository;
    
    public OrderService(IOrderRepository orderRepository, IShoppingCartRepository cartRepository)
    {
        _orderRepository = orderRepository;
        _shoppingCartRepository = cartRepository;
    }
    
    public Task<IEnumerable<OrderDto>> GetOrdersAsync(string email)
    {
        return  _orderRepository.GetOrdersAsync(email);
    }

    public Task CreateNewOrderAsync(string email, OrderInputModel order)
    {
        
        
        _orderRepository.CreateNewOrderAsync(email, order);
        _shoppingCartRepository.DeleteCartAsync(email);
        
        //TODO:
        /*
            Publish a message to RabbitMQ with the routing key ‘create-order’ and include the
            newly created order
         */
        return Task.CompletedTask;
    }
}