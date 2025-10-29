using Cryptocop.Models.InputModels;
using Cryptocop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.WebApi.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("/register")]
    public IActionResult CreateUserAsync([FromBody] RegisterInputModel model)
    {
        return Ok(model);
    }

    [HttpPost("/signin")]
    public IActionResult AuthenticateUserAsync([FromBody] LoginInputModel model)
    {
        return Ok(model);
    }

    [HttpGet("logout")]
    public async Task<IActionResult> LogoutAsync(int tokenId)
    {
        await _accountService.LogoutAsync(tokenId);
        return Ok("User logged out");
    }

}