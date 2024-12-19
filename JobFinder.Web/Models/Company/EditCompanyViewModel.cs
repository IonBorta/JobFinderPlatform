using System.ComponentModel.DataAnnotations;
using JobFinder.Core.Enums;
using JobFinder.Web.Models.User;

namespace JobFinder.Web.Models.Company
{
    public class EditCompanyViewModel: BaseViewModel
    {
        [Required(ErrorMessage = "A description for company is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Nr. of workers is required")]
        public int Workers { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Domain is required")]
        public CompanyDomains Domain { get; set; }
        public string City { get; set; }
        public string Logo { get; set; } = "https://icons.veryicon.com/png/o/miscellaneous/zr_icon/company-23.png";
        public GetUserViewModel User {  get; set; }
    }
}
