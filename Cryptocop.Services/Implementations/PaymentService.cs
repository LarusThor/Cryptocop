using Cryptocop.Models.Dtos;
using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Interfaces;
using Cryptocop.Services.Interfaces;

namespace Cryptocop.Services.Implementations;

public class PaymentService : IPaymentService
{
    private readonly IPaymentRepository _paymentRepository;

    public PaymentService(IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }
    
    public Task AddPaymentCardAsync(string email, PaymentCardInputModel paymentCard)
    {
        return  _paymentRepository.AddPaymentCardAsync(email, paymentCard);
    }

    public Task<IEnumerable<PaymentCardDto>> GetStoredPaymentCardsAsync(string email)
    {
        return  _paymentRepository.GetStoredPaymentCardsAsync(email);
    }
}