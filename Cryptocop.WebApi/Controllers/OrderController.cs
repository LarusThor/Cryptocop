using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Interfaces;
using Cryptocop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.WebApi.Controllers;

[Route("api/orders")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService  _orderService;
    
    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpGet]
    public async Task<IActionResult> GetOrdersAsync(string email)
    {
        await _orderService.GetOrdersAsync(email);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> CreateNewOrderAsync(string email, [FromBody] OrderInputModel order)
    {
        await _orderService.CreateNewOrderAsync(email, order);
        return Ok();
    }
}