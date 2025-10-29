using Cryptocop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.WebApi.Controllers;

[ApiController]
[Route("api/cryptocurrencies")]
public class CryptoCurrencyController : ControllerBase
{
    private readonly ICryptoCurrencyService _cryptoCurrencyService;

    public CryptoCurrencyController(ICryptoCurrencyService cryptoCurrencyService)
    {
        _cryptoCurrencyService = cryptoCurrencyService;
    }
    
    [HttpGet]
    public async Task<ActionResult> GetAvailableCryptocurrenciesAsync()
    {
        var cryptoCurrencies = await _cryptoCurrencyService.GetAvailableCryptocurrenciesAsync();
        return Ok(cryptoCurrencies);
    }
}