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

    public decimal Quantity { get; set; }

    public decimal UnitPrice { get; set; }
}