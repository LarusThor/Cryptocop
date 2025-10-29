using Cryptocop.Models.Dtos;
using Cryptocop.Models.Entities;
using Cryptocop.Models.InputModels;

namespace Cryptocop.Repositories.Interfaces;

public interface IPaymentRepository
{
    Task AddPaymentCardAsync(string email, PaymentCardInputModel paymentCard);
    Task<IEnumerable<PaymentCardDto>> GetStoredPaymentCardsAsync(string email);
}