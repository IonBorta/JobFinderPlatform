using JobFinder.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Email is required")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
    public class UserViewModel : LoginViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The Full Name is required")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Confirm yor password")]
        public string ConfirmPassword { get; set; }
        public UserType UserType { get; set; }
    }
}
