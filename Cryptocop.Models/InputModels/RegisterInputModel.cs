using System.ComponentModel.DataAnnotations;

namespace Cryptocop.Models.InputModels;

public class RegisterInputModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }  = null!;
    [Required]
    [MinLength(3)]
    public string FullName { get; set; }  = null!;
    [Required]
    [MinLength(8)]
    public string Password { get; set; }  = null!;
    [Required]
    [MinLength(8)]
    [Compare("Password")]
    public string PasswordConfirmation { get; set; }  = null!;
}