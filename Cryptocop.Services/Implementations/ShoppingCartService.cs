using Cryptocop.Models.Dtos;
using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Interfaces;
using Cryptocop.Services.Interfaces;

namespace Cryptocop.Services.Implementations;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
    {
        _shoppingCartRepository = shoppingCartRepository;
    }
    
    public Task<IEnumerable<ShoppingCartItemDto>> GetCartItemsAsync(string email)
    {
        return  _shoppingCartRepository.GetCartItemsAsync(email);
    }

    //TODO: Figure out API Call thing
    public Task AddCartItemAsync(string email, ShoppingCartItemInputModel shoppingCartItemItem)
    {
        // need shoppingCartItemItem.ProductIdentifier to get latest usd price for item
    }

    public Task RemoveCartItemAsync(string email, int id)
    {
        return  _shoppingCartRepository.RemoveCartItemAsync(email, id);
    }

    public Task UpdateCartItemQuantityAsync(string email, int id, float quantity)
    {
        return  _shoppingCartRepository.UpdateCartItemQuantityAsync(email, id, quantity);
    }

    public Task ClearCartAsync(string email)
    {
        return _shoppingCartRepository.ClearCartAsync(email);
    }
}