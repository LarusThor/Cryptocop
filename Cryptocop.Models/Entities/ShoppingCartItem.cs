using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cryptocop.Models.Entities;

public class ShoppingCartItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int ShoppingCartId { get; set; }

    [ForeignKey(nameof(ShoppingCartId))]
    public ShoppingCart ShoppingCart { get; set; }
    
    public string ProductIdentifier { get; set; }

    public float? Quantity { get; set; }

    public float UnitPrice { get; set; }
}