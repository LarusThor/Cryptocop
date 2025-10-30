using System.ComponentModel.DataAnnotations;

namespace Cryptocop.Models.InputModels;

public class ShoppingCartItemInputModel
{
    [Required]
    public string ProductIdentifier { get; set; }  = null!;
    [Required]
    [Range(0.01, float.MaxValue)]
    public float? Quantity { get; set; }
}