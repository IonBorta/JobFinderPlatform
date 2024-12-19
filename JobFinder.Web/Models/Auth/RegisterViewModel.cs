using System.ComponentModel.DataAnnotations;
using JobFinder.Web.Models.User;

namespace JobFinder.Web.Models.Auth
{
    public class RegisterViewModel: LoginViewModel
    {
        public RegisterViewModel()
        {
            Created = DateTime.Now;
        }
        [Required(ErrorMessage = "Confirm the Password")]
        public string ConfirmPassword { get; set; }
        public DateTime Created { get; set; }
    }
}
