using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cryptocop.Models.Entities;

public class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    // Should be foreign key
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }  = null!;

    public string StreetName { get; set; } = null!;

    public string HouseNumber {get; set;} = null!;

    public string ZipCode {get; set;} = null!;

    public string Country {get; set;} = null!;

    public string City {get; set;} = null!;
}