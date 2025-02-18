using System.ComponentModel.DataAnnotations;

namespace Team7MVC.Models
{
    public class LoginViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "*")]
        [MaxLength(255, ErrorMessage = "Must under 255 characters")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Display(Name = "Password")]
        [Required(ErrorMessage = "*")]
        [MaxLength(255, ErrorMessage = "Must under 255 characters")]
        public string Password { get; set; }
    }
}
