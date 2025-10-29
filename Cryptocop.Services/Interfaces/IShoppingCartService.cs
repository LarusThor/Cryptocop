using Cryptocop.Models.Dtos;
using Cryptocop.Models.InputModels;

namespace Cryptocop.Services.Interfaces;

public interface IShoppingCartService
{
    Task<IEnumerable<ShoppingCartItemDto>> GetCartItemsAsync(string email);
    Task AddCartItemAsync(string email, ShoppingCartItemInputModel shoppingCartItemItem);
    Task RemoveCartItemAsync(string email, int id);
    Task UpdateCartItemQuantityAsync(string email, int id, float quantity);
    Task ClearCartAsync(string email);
}