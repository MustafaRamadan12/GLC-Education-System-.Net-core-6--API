using System.ComponentModel.DataAnnotations;

namespace GLC.Core.ViewModels
{
  public class SignUpVM
  {
    [Required]
    [MaxLength(50)]
    public string? UserName { get; set; }
    [Required(ErrorMessage = "ThisField Required")]
    [EmailAddress(ErrorMessage = "Invalid Mail")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "ThisField Required")]
    [MinLength(6, ErrorMessage = "Min Length 6")]
    public string? Password { get; set; }
    [Required(ErrorMessage = "ThisField Required")]
    [MinLength(6, ErrorMessage = "Min Length 6")]
    [Compare("Password", ErrorMessage = "Password Not match")]
    public string? ConfirmPassword { get; set; }
    [Required]
    public string? Address { get; set; }
    [Required]
    public string? ParentEmail { get; set; }
    [Required]
    public int Age { get; set; }
    [Required]
    public int Level { get; set; }

    [Required]
    public string Gender { get; set; }
  }
}
