using System.ComponentModel.DataAnnotations;

namespace Cryptocop.Models.InputModels;

public class PaymentCardInputModel
{
    [Required]
    [MinLength(3)]
    public string CardholderName  { get; set; }  = null!;
    [Required]
    [CreditCard]
    public string CardNumber { get; set; }  = null!;
    [Range(1, 12)]
    public int Month { get; set; }
    [Range(0, 99)]
    public int Year { get; set; }
}