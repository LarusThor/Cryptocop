using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cryptocop.Models.Entities;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Email { get; set; }  = null!;

    public string FullName { get; set; }  = null!;

    public string StreetName { get; set; }  = null!;

    public string HouseNumber { get; set; }  = null!;

    public string ZipCode { get; set; }  = null!;

    public string Country { get; set; } = null!;

    public string City { get; set; }  = null!;

    public string CardholderName { get; set; } = null!;

    public string MaskedCreditCard { get; set; } = null!;

    public DateTime OrderDate { get; set; }

    public float TotalPrice { get; set; }

}