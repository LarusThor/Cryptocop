
namespace Cryptocop.Models.Entities;

public class PaymentCard
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    // Should be foreign key
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    public string CardholderName { get; set; }

    public string CardNumber { get; set; }

    public int Month { get; set; }

    public int Year { get; set; }
}
