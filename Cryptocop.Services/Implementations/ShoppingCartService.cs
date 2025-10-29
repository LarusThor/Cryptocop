using System.Net.Http.Headers;
using Cryptocop.Models.Dtos;
using Cryptocop.Models.InputModels;
using Cryptocop.Repositories.Interfaces;
using Cryptocop.Services.Helpers;
using Cryptocop.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cryptocop.Services.Implementations;

public class ShoppingCartService : IShoppingCartService
{
    private readonly IShoppingCartRepository _shoppingCartRepository;
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ShoppingCartService(IShoppingCartRepository shoppingCartRepository, HttpClient httpClient, IConfiguration configuration)
    {
        _shoppingCartRepository = shoppingCartRepository;
        
        _httpClient = httpClient;
        _configuration = configuration;

        _httpClient.BaseAddress = new Uri("https://data.messari.io/api");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        var apiKey = _configuration["Cryptocop:ApiKey"];
        _httpClient.DefaultRequestHeaders.Add("baFKS4in-aQwbDpg+zbRYVbfbeVrpG6rxMpNxIeTGWxMXD+g", apiKey);
    }
    
    public async Task<IEnumerable<ShoppingCartItemDto>> GetCartItemsAsync(string email)
    {
        return  await _shoppingCartRepository.GetCartItemsAsync(email);
    }
    
    public async Task AddCartItemAsync(string email, ShoppingCartItemInputModel shoppingCartItemItem)
    {
        var response = await _httpClient.GetAsync("/v2/assets");
        response.EnsureSuccessStatusCode();
        
        var cryptoList = await response.DeserializeJsonToList<CryptoCurrencyDto>(flatten:true);

        var product = cryptoList.FirstOrDefault(p => p.Symbol == shoppingCartItemItem.ProductIdentifier);

        var currentPrice = product?.PriceInUsd ?? 0f;

        await _shoppingCartRepository.AddCartItemAsync(email, shoppingCartItemItem, currentPrice);
    }

    public async Task RemoveCartItemAsync(string email, int id)
    {
        await _shoppingCartRepository.RemoveCartItemAsync(email, id);
    }

    public async Task UpdateCartItemQuantityAsync(string email, int id, float quantity)
    {
        await _shoppingCartRepository.UpdateCartItemQuantityAsync(email, id, quantity);
    }

    public async Task ClearCartAsync(string email)
    {
        await _shoppingCartRepository.ClearCartAsync(email);
    }
}