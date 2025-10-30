namespace Cryptocop.Models.Dtos;

public class AddressDto
{
    public int Id { get; set; }
    
    public string StreetName { get; set; }  = null!;

    public string HouseNumber { get; set; }  = null!;
    
    public string ZipCode { get; set; }  = null!;
    
    public string Country { get; set; }  = null!;
    
    public string City { get; set; }  = null!;

}