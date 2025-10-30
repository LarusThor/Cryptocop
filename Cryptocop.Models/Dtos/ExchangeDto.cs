using System.Text.Json.Serialization;
using Newtonsoft.Json;
namespace Cryptocop.Models.Dtos;

public class ExchangeDto
{
    [JsonProperty("id")]
    public string Id { get; set; }  = null!;
    [JsonProperty("exchange_name")]
    public string Name { get; set; }  = null!;
    [JsonProperty("exchange_slug")]
    public string Slug { get; set; }  = null!;
    [JsonProperty("base_asset_symbol")]
    public string AssetSymbol  { get; set; }  = null!;
    [JsonProperty("price_usd")]
    public float? PriceInUsd  { get; set; }
    
    public DateTime? LastTrade { get; set; }
}