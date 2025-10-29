using Cryptocop.Models.Dtos;
using Cryptocop.Models.InputModels;

namespace Cryptocop.Services.Interfaces;

public interface IPaymentService
{
    Task AddPaymentCardAsync(string email, PaymentCardInputModel paymentCard);
    Task<IEnumerable<PaymentCardDto>> GetStoredPaymentCardsAsync(string email);
}