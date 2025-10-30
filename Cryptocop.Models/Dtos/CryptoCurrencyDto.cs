namespace Cryptocop.Models.Dtos;

public class CryptoCurrencyDto
{
    public string Id { get; set; }  = null!;
    
    public string Symbol { get; set; }  = null!;
    
    public string Name { get; set; }  = null!;
    
    public string Slug { get; set; }  = null!;
    
    public float PriceInUsd { get; set; }
    
    public string ProjectDetails  { get; set; }  = null!;
}