using Cryptocop.Models.InputModels;
using Cryptocop.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Cryptocop.WebApi.Controllers;

[Route("api/payments")]
[ApiController]
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
        await _paymentService.GetStoredPaymentCardsAsync(email);
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> AddPaymentCardAsync(string email, [FromBody]PaymentCardInputModel paymentCard)
    {
        await _paymentService.AddPaymentCardAsync(email, paymentCard);
        return Ok();
    }
}