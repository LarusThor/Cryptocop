using Cryptocop.Models.Dtos;
using Cryptocop.Models.InputModels;

namespace Cryptocop.Services.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDto>> GetOrdersAsync(string email);
    Task CreateNewOrderAsync(string email, OrderInputModel order);
}