using System.ComponentModel.DataAnnotations;

namespace Cryptocop.Models.InputModels;

public class AddressInputModel
{
    [Required]
    public string StreetName { get; set; }  = null!;
    [Required]
    public string HouseNumber { get; set; }  = null!;
    [Required]
    public string ZipCode { get; set; }  = null!;
    [Required]
    public string Country { get; set; }  = null!;
    [Required]
    public string City { get; set; }  = null!;
}