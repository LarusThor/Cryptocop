namespace Cryptocop.Models.Dtos;

public class OrderDto
{
    public int Id { get; set; }
    
    public string Email { get; set; }  = null!;
    
    public string FullName { get; set; }  = null!;
    
    public string StreetName { get; set; }  = null!;
    
    public string HouseNumber { get; set; }  = null!;
    
    public string ZipCode { get; set; }  = null!;
    
    public string Country { get; set; }  = null!;
    
    public string City { get; set; }  = null!;
    
    public string CardholderName  { get; set; }  = null!;
    
    public string CreditCard { get; set; }  = null!;
    
    public DateTime OrderDate  { get; set; }
    
    public float TotalPrice  { get; set; }
    
    public List<OrderItemDto> OrderItems { get; set; }  = null!;
}