
namespace Cryptocop.Models.Entities;

public class Address
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    // Should be foreign key
    public int UserId { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }

    public string StreetName {get; set;}

    public string HouseNumber {get; set;}

    public string ZipCode {get; set;}

    public string Country {get; set;}

    public string City {get; set;}
}