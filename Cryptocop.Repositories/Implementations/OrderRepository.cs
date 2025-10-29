using Cryptocop.Models.Dtos;
using Cryptocop.Models.Entities;
using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Data;
using Cryptocop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cryptocop.Repositories.Implementations;

public class OrderRepository : IOrderRepository
{
    public readonly CryptocopDbContext _dbcontext;

    public OrderRepository(CryptocopDbContext dbContext)
    {
        _dbcontext = dbContext;
    }
    
    public async Task<IEnumerable<OrderDto>> GetOrdersAsync(string email)
    {
        var user =  _dbcontext.Users.FirstOrDefault(u => u.Email == email);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        var orders = _dbcontext.Orders
            .Where(o => o.Email == user.Email)
            .Select(o => new OrderDto()
            {
                Id = o.Id,
                Email = o.Email,
                FullName = o.FullName,
                StreetName = o.StreetName,
                HouseNumber = o.HouseNumber,
                ZipCode = o.ZipCode,
                Country = o.Country,
                City = o.City,
                CardholderName = o.CardholderName,
                CreditCard = o.MaskedCreditCard,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice
            });
        return await orders.ToListAsync();
    }

    public async Task<OrderDto> CreateNewOrderAsync(string email, OrderInputModel order)
    {
        var userInfo = await _dbcontext.Users
            .FirstOrDefaultAsync(u => u.Email ==  email);

        if (userInfo == null)
        {
            throw new Exception("User not found");
        }

        var addrInfo = await _dbcontext.Addresses
            .FirstOrDefaultAsync(a => a.Id == order.AddressId && a.UserId == userInfo.Id);
        
        if (addrInfo == null)
        {
            throw new Exception("Address not found");
        }

        var paymentCardInfo = await _dbcontext.PaymentCards
            .FirstOrDefaultAsync(p => p.Id == order.PaymentCardId && p.UserId == userInfo.Id);
        
        if (paymentCardInfo == null)
        {
            throw new Exception("Payment card not found");
        }
        
        var cart = await _dbcontext.ShoppingCarts
            .FirstOrDefaultAsync(c => c.UserId == userInfo.Id);

        var cartItems = await _dbcontext.ShoppingCartItems
            .Where(i => i.ShoppingCartId == cart.Id)
            .ToListAsync();
        
        float totalPrice = cartItems.Sum(i => (float)i.Quantity * (float)i.UnitPrice);
        
        string maskedCreditCardNumber = new string('*', paymentCardInfo.CardNumber.Length - 4)
            + paymentCardInfo.CardNumber[^4..];

        var newOrder = new Order()
        {
            Email = userInfo.Email,
            StreetName = addrInfo.StreetName,
            HouseNumber = addrInfo.HouseNumber,
            ZipCode = addrInfo.ZipCode,
            Country = addrInfo.Country,
            City = addrInfo.City,
            CardholderName = paymentCardInfo.CardholderName,
            MaskedCreditCard = maskedCreditCardNumber,
            TotalPrice = totalPrice,
            OrderDate = DateTime.UtcNow
        };
        
        _dbcontext.Orders.Add(newOrder);
        await _dbcontext.SaveChangesAsync();

        var orderDto = new OrderDto
        {
            Id = newOrder.Id,
            Email = newOrder.Email,
            FullName = newOrder.FullName,
            StreetName = newOrder.StreetName,
            HouseNumber = newOrder.HouseNumber,
            ZipCode = newOrder.ZipCode,
            Country = newOrder.Country,
            City = newOrder.City,
            CardholderName = newOrder.CardholderName,
            CreditCard = paymentCardInfo.CardNumber,
            OrderDate = newOrder.OrderDate,
            OrderItems = new List<OrderItemDto>()
        };

        foreach (var item in cartItems)
        {
            orderDto.OrderItems.Add(new OrderItemDto()
            {
                Id = item.Id,
                ProductIdentifier =  item.ProductIdentifier,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.Quantity * item.UnitPrice
            });
        }
        return orderDto;
    }
}