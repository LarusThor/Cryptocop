using Cryptocop.Models.Dtos;
using Cryptocop.Models.Entities;
using Cryptocop.Models.InputModels;

namespace Cryptocop.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<IEnumerable<OrderDto>> GetOrdersAsync(string email);
    Task<OrderDto> CreateNewOrderAsync(string email, OrderInputModel order);
}