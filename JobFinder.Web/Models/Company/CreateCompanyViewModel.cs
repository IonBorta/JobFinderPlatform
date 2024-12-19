using JobFinder.Core.Enums;
using System.ComponentModel.DataAnnotations;
using JobFinder.Web.Models.User;
using JobFinder.Web.Models.Auth;

namespace JobFinder.Web.Models.Company
{
    public class CreateCompanyViewModel : CreateUserViewModel
    {
        public CreateCompanyViewModel()
        {
            UserType = UserType.Employer;
        }
        [StringLength(9, MinimumLength = 9, ErrorMessage = "9 digits required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Domain is required")]
        public CompanyDomains Domain { get; set; }
    }
}
