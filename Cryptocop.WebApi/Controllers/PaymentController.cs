using Cryptocop.Models.InputModels;
using Cryptocop.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.WebApi.Controllers;

[Route("api/payments")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PaymentController : ControllerBase
{
    private readonly IPaymentService _paymentService;
    
    public PaymentController(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetStoredPaymentCardsAsync(string email)
    {
        var storedPayments = await _paymentService.GetStoredPaymentCardsAsync(email);
        return Ok(storedPayments);
    }

    [HttpPost]
    public async Task<IActionResult> AddPaymentCardAsync(string email, [FromBody]PaymentCardInputModel paymentCard)
    {
        await _paymentService.AddPaymentCardAsync(email, paymentCard);
        return Ok();
    }
}