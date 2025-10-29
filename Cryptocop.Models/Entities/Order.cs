using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cryptocop.Models.Entities;

public class Order
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Email { get; set; }

    public string FullName { get; set; }

    public string StreetName { get; set; }

    public string HouseNumber { get; set; }

    public string ZipCode { get; set; }

    public string Country { get; set; }

    public string City { get; set; }

    public string CardholderName { get; set; }

    public string MaskedCreditCard { get; set; }

    public DateTime OrderDate { get; set; }

    public float TotalPrice { get; set; }

}