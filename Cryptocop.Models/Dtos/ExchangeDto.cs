namespace Cryptocop.Models.Dtos;

public class ExchangeDto
{
    public string Id { get; set; }  = null!;
    
    public string Name { get; set; }  = null!;
    
    public string Slug { get; set; }  = null!;
    
    public string AssetSymbol  { get; set; }  = null!;
    
    public float? PriceInUsd  { get; set; }
    
    public DateTime? LastTrade { get; set; }
}