using Cryptocop.Models.Dtos;

namespace Cryptocop.Services.Interfaces;

public interface ICryptoCurrencyService
{
    Task<IEnumerable<CryptoCurrencyDto>> GetAvailableCryptocurrenciesAsync();
}