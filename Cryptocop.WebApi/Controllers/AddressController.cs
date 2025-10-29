using Cryptocop.Models.InputModels;
using Cryptocop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.WebApi.Controllers;

[Route("api/addresses")]
[ApiController]
public class AddressController : ControllerBase
{
    private readonly IAddressService  _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAddressesAsync(string email)
    {
        await _addressService.GetAllAddressesAsync(email);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddAddressAsync(string email, AddressInputModel address)
    {
        await _addressService.AddAddressAsync(email, address);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAddressAsync(string email, int id)
    {
        await _addressService.DeleteAddressAsync(email, id);
        return Ok();
    }
}