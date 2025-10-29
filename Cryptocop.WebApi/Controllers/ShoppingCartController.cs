using Cryptocop.Models.InputModels;
using Cryptocop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.WebApi.Controllers;

[Route("api/cart")]
[ApiController]
public class ShoppingCartController : ControllerBase
{
    private readonly IShoppingCartService _shoppingCartService;

    public ShoppingCartController(IShoppingCartService shoppingCartService)
    {
        _shoppingCartService = shoppingCartService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCartItemsAsync(string email)
    {
        var items = await _shoppingCartService.GetCartItemsAsync(email);
        return Ok(items);
    }

    [HttpPost]
    public async Task<IActionResult> AddCartItemAsync(string email,
        [FromBody] ShoppingCartItemInputModel shoppingCartItemInputModel)
    {
        await _shoppingCartService.AddCartItemAsync(email, shoppingCartItemInputModel);
        return Ok("Item added to cart");
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveCartItemAsync(string email, int id)
    {
        await _shoppingCartService.RemoveCartItemAsync(email, id);
        return Ok("Item removed from cart");
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateCartItemQuantityAsync(string email, int id, float quantity)
    {
        await _shoppingCartService.UpdateCartItemQuantityAsync(email, id, quantity);
        return Ok("Quantity updated from cart");
    }

    [HttpDelete]
    public async Task<IActionResult> ClearCartAsync(string email)
    {
        await _shoppingCartService.ClearCartAsync(email);
        return Ok("Cart cleared");
    }
}