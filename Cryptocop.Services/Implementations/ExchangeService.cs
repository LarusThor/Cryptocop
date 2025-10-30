using System.Net.Http.Headers;
using Cryptocop.Models;
using Cryptocop.Models.Dtos;
using Cryptocop.Services.Helpers;
using Cryptocop.Services.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Cryptocop.Services.Implementations;

public class ExchangeService : IExchangeService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ExchangeService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
        
        _httpClient.BaseAddress = new Uri("https://data.messari.io/api/v1/");
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        
        var apiKey = _configuration["Cryptocop:ApiKey"];
        _httpClient.DefaultRequestHeaders.Add("x-messari-api-key", apiKey);
    }
    
    public async Task<Envelope<ExchangeDto>> GetExchangesAsync(int pageNumber = 1)
    {
        int pageSize = 20;
        var response = await _httpClient.GetAsync($"markets?page={pageNumber}");
        response.EnsureSuccessStatusCode();

        var marketList = await response.DeserializeJsonToList<ExchangeDto>();
        var envelope = new Envelope<ExchangeDto>
        {
            Items = marketList,
            PageNumber = pageNumber,
        };
        return envelope;

    }
}