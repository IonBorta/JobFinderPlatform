using System.ComponentModel.DataAnnotations;
using JobFinder.Core.Enums;
using JobFinder.Web.Models.Auth;

namespace JobFinder.Web.Models.User
{
    public class CreateUserViewModel: RegisterViewModel
    {
        [Required(ErrorMessage = "The Full Name is required")]
        public string Name { get; set; }
        public UserType UserType { get; set; } = UserType.Employee;
    }
}
