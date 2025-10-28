
namespace Cryptocop.Models.Entities;

public class JwtToken
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public bool Blacklisted { get; set; }

}