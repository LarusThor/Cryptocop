using Cryptocop.Models.Dtos;
using Cryptocop.Models.Entities;
using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Data;
using Cryptocop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cryptocop.Repositories.Implementations;

public class ShoppingCartRepository : IShoppingCartRepository
{
    private readonly CryptocopDbContext  _dbcontext;

    public ShoppingCartRepository(CryptocopDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    
    public async Task<IEnumerable<ShoppingCartItemDto>> GetCartItemsAsync(string email)
    {
        var user = await _dbcontext.Users
            .FirstOrDefaultAsync(user => user.Email == email);

        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var cart = await _dbcontext.ShoppingCarts
            .FirstOrDefaultAsync(c => c.UserId == user.Id);
        
        if (cart == null)
        {
            throw new Exception("Shopping cart not found");
        }

        var cartItems = await _dbcontext.ShoppingCartItems
            .Where(i => i.ShoppingCartId == cart.Id)
            .ToListAsync();
        
        var cartItemDtos = new List<ShoppingCartItemDto>();
        
        foreach (var item in cartItems)
        {
            cartItemDtos.Add(new ShoppingCartItemDto()
            {
                Id = item.Id,
                ProductIdentifier =  item.ProductIdentifier,
                Quantity = item.Quantity,
                UnitPrice = item.UnitPrice,
                TotalPrice = item.Quantity * item.UnitPrice
            });
        }
        return  cartItemDtos;
    }

    public async Task AddCartItemAsync(string email, ShoppingCartItemInputModel shoppingCartItemItem, float priceInUsd)
    {
        var user = await _dbcontext.Users
            .FirstOrDefaultAsync(user => user.Email == email);

        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var cart = await _dbcontext.ShoppingCarts
            .FirstOrDefaultAsync(c => c.UserId == user.Id);
        
        if (cart == null)
        {
            throw new Exception("Shopping cart not found");
        }

        var shoppingCartItem = new ShoppingCartItem
        {
            ShoppingCartId = cart.Id,
            ProductIdentifier = shoppingCartItemItem.ProductIdentifier,
            Quantity = shoppingCartItemItem.Quantity,
            UnitPrice = priceInUsd
        };
        _dbcontext.ShoppingCartItems.Add(shoppingCartItem);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task RemoveCartItemAsync(string email, int id)
    {
        var user = await _dbcontext.Users
            .FirstOrDefaultAsync(user => user.Email == email);

        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var cart = await _dbcontext.ShoppingCarts
            .FirstOrDefaultAsync(c => c.UserId == user.Id);
        
        if (cart == null)
        {
            throw new Exception("Shopping cart not found");
        }
        
        var cartItem = await  _dbcontext.ShoppingCartItems
            .FirstOrDefaultAsync(i => i.ShoppingCartId == cart.Id && i.Id  == id);

        if (cartItem == null)
        {
            throw new Exception("item not found in cart");
        }
        
        _dbcontext.ShoppingCartItems.Remove(cartItem);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task UpdateCartItemQuantityAsync(string email, int id, float quantity)
    {
        var user = await _dbcontext.Users
            .FirstOrDefaultAsync(user => user.Email == email);

        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var cart = await _dbcontext.ShoppingCarts
            .FirstOrDefaultAsync(c => c.UserId == user.Id);
        
        if (cart == null)
        {
            throw new Exception("Shopping cart not found");
        }
        
        var item = await _dbcontext.ShoppingCartItems
            .FirstOrDefaultAsync(i => i.ShoppingCartId == cart.Id && i.Id ==id);

        if (item == null)
        {
            throw new Exception("item not found in cart");
        }
        
        item.Quantity = quantity;
        await _dbcontext.SaveChangesAsync();
    }

    public async Task ClearCartAsync(string email)
    {
        var user = await _dbcontext.Users
            .FirstOrDefaultAsync(user => user.Email == email);

        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var cart = await _dbcontext.ShoppingCarts
            .FirstOrDefaultAsync(c => c.UserId == user.Id);
        
        if (cart == null)
        {
            throw new Exception("Shopping cart not found");
        }

        var cartItems = await _dbcontext.ShoppingCartItems
            .Where(i => i.ShoppingCartId == cart.Id)
            .ToListAsync();

        _dbcontext.ShoppingCartItems.RemoveRange(cartItems);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task DeleteCartAsync(string email)
    {
        var user = await _dbcontext.Users
            .FirstOrDefaultAsync(user => user.Email == email);

        if (user == null)
        {
            throw new Exception("User not found");
        }
        
        var cart = await _dbcontext.ShoppingCarts
            .FirstOrDefaultAsync(c => c.UserId == user.Id);
        
        if (cart == null)
        {
            throw new Exception("Shopping cart not found");
        }
        
        _dbcontext.ShoppingCarts.Remove(cart);
        await _dbcontext.SaveChangesAsync();
    }
}