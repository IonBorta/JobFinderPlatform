using System.ComponentModel.DataAnnotations;
using JobFinder.Web.Models.User;

namespace JobFinder.Web.Models.Auth
{
    public class RegisterViewModel: CreateUserViewModel
    {
        [Required(ErrorMessage = "Confirm the Password")]
        public string ConfirmPassword { get; set; }
    }
}
