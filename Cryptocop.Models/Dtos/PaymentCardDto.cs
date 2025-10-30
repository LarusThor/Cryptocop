namespace Cryptocop.Models.Dtos;

public class PaymentCardDto
{
    public int Id { get; set; }
    
    public string CardholderName { get; set; }  = null!;
    
    public string CardNumber { get; set; }  = null!;
    
    public int Month { get; set; }
    
    public int Year { get; set; }
}