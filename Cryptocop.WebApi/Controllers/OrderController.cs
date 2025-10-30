using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Interfaces;
using Cryptocop.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.WebApi.Controllers;

[Route("api/orders")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        var orders =await _orderService.GetOrdersAsync(email);
        return Ok(orders);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateNewOrderAsync(string email, [FromBody]OrderInputModel order)
    {
        await _orderService.CreateNewOrderAsync(email, order);
        return Ok();
    }
}