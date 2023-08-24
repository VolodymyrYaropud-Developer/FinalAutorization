using System.ComponentModel.DataAnnotations;

namespace FinalAutorization.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Email Name is required")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
