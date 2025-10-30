using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Interfaces;
using Cryptocop.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.WebApi.Controllers;

[Route("api/account")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;
    private readonly ITokenService _tokenService;
    
    public AccountController(IAccountService accountService, ITokenService  tokenService)
    {
        _accountService = accountService;
        _tokenService = tokenService;
    }

    // No authentication required
    [HttpPost("register")]
    public async Task<IActionResult> CreateUserAsync([FromBody] RegisterInputModel model)
    {
        var user = await _accountService.CreateUserAsync(model);
        if (user == null)
        {
            return BadRequest();
        }
        return Ok(user);
    }

    // No authentication required
    [HttpPost("signin")]
    public async Task<IActionResult> AuthenticateUserAsync([FromBody] LoginInputModel model)
    {
        var user = await  _accountService.AuthenticateUserAsync(model);
        if (user == null)
        {
            return Unauthorized();
        }
        var jwt = await _tokenService.GenerateJwtTokenAsync(user);
        return Ok(new { token = jwt });
    }

    //TODO: Might need to tweak
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [HttpGet("logout")]
    public async Task<IActionResult> LogoutAsync(int tokenId)
    {
        await _accountService.LogoutAsync(tokenId);
        return Ok("User logged out");
    }

}