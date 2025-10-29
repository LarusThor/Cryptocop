using Cryptocop.Models.Dtos;
using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Interfaces;
using Cryptocop.Services.Interfaces;

namespace Cryptocop.Services.Implementations;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderRepository;

    private readonly IShoppingCartRepository _shoppingCartRepository;
    
    private readonly IQueueService _queueService;
    public OrderService(IOrderRepository orderRepository, IShoppingCartRepository cartRepository,  IQueueService queueService)
    {
        _orderRepository = orderRepository;
        _shoppingCartRepository = cartRepository;
        _queueService = queueService;
    }
    
    public async Task<IEnumerable<OrderDto>> GetOrdersAsync(string email)
    {
        return await _orderRepository.GetOrdersAsync(email);
    }

    public async Task CreateNewOrderAsync(string email, OrderInputModel order)
    {
        var newOrder = _orderRepository.CreateNewOrderAsync(email, order);
        await _shoppingCartRepository.DeleteCartAsync(email);
        await _queueService.PublishMessageAsync("create-order", newOrder);
    }
}