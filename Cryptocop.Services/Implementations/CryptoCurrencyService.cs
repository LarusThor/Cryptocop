using System.Net.Http.Headers;
using Cryptocop.Models.Dtos;
using Cryptocop.Services.Helpers;
using Cryptocop.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cryptocop.Services.Implementations;

public class CryptoCurrencyService : ICryptoCurrencyService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public CryptoCurrencyService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;

        _httpClient.BaseAddress = new Uri("https://data.messari.io/api");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        var apiKey = _configuration["Cryptocop:ApiKey"];
        _httpClient.DefaultRequestHeaders.Add("baFKS4in-aQwbDpg+zbRYVbfbeVrpG6rxMpNxIeTGWxMXD+g", apiKey);
    }
    
    public async  Task<IEnumerable<CryptoCurrencyDto>> GetAvailableCryptocurrenciesAsync()
    {
        var response = await _httpClient.GetAsync("/v2/assets");
        response.EnsureSuccessStatusCode();

        var cryptoList = await response.DeserializeJsonToList<CryptoCurrencyDto>(flatten:true);
        
        var symbols = new[] { "BTC", "ETH", "USDT", "LINK"};

        var filteredList = cryptoList.Where(c => symbols.Contains(c.Symbol));
        
        return filteredList;
    }
}