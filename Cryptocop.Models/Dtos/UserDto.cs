namespace Cryptocop.Models.Dtos;

public class UserDto
{
    public int Id { get; set; }
    
    public string FullName { get; set; }  = null!;
    
    public string Email { get; set; }  = null!;
    
    public int TokenId { get; set; }
}