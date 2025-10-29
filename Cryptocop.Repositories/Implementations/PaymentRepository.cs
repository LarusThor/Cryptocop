using Cryptocop.Models.Dtos;
using Cryptocop.Models.Entities;
using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Data;
using Cryptocop.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Cryptocop.Repositories.Implementations;

public class PaymentRepository : IPaymentRepository
{
    private readonly CryptocopDbContext _dbcontext;

    public PaymentRepository(CryptocopDbContext dbcontext)
    {
        _dbcontext = dbcontext;
    }
    
    public async Task AddPaymentCardAsync(string email, PaymentCardInputModel paymentCard)
    {
        var user = await _dbcontext.Users
            .FirstOrDefaultAsync(u => u.Email == email);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }

        var newPaymentCard = new PaymentCard
        {
            UserId = user.Id,
            CardholderName = paymentCard.CardholderName,
            CardNumber = paymentCard.CardNumber,
            Month = paymentCard.Month,
            Year = paymentCard.Year,
        };
        _dbcontext.PaymentCards.Add(newPaymentCard);
        await _dbcontext.SaveChangesAsync();
    }

    public async Task<IEnumerable<PaymentCardDto>> GetStoredPaymentCardsAsync(string email)
    {
        var user = await _dbcontext.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
        {
            throw new Exception("User not found");
        }

        var PaymentCards = _dbcontext.PaymentCards
            .Where(pc => pc.UserId == user.Id)
            .Select(p => new PaymentCardDto()
            {
                Id = p.Id,
                CardholderName = p.CardholderName,
                CardNumber = p.CardNumber,
                Month = p.Month,
                Year = p.Year,
            });
        return await PaymentCards.ToListAsync();
    }
}