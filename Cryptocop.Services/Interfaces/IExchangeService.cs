using Cryptocop.Models;
using Cryptocop.Models.Dtos;

namespace Cryptocop.Services.Interfaces;

public interface IExchangeService
{
    Task<Envelope<ExchangeDto>> GetExchangesAsync(int pageNumber = 1);
}