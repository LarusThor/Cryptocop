using Cryptocop.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.WebApi.Controllers;

[Route("api/exchanges")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ExchangeController : ControllerBase
{
    private readonly IExchangeService _exchangeService;
    
    public ExchangeController(IExchangeService exchangeService)
    {
        _exchangeService = exchangeService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetExchangesAsync([FromQuery] int pageNumber = 1)
    {
        var exchanges = await _exchangeService.GetExchangesAsync(pageNumber);
        return Ok(exchanges);
    }
}