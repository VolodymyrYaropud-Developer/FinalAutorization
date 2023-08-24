using System.ComponentModel.DataAnnotations;

namespace FinalAutorization.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string userName { get; set; }
       
        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        [Required(ErrorMessage ="Password is different")]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
    }
}
