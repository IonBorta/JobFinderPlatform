using System.ComponentModel.DataAnnotations;
using JobFinder.Web.Validations;

namespace JobFinder.Web.Models.Auth
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [PasswordValidation]
        public string Password { get; set; }
    }
}
