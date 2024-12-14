using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.Company
{
    public class EditCompanyViewModel:BaseViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "A description for company is required")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Nr. of workers is required")]
        public int Workers { get; set; }
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }
        public string Logo { get; set; }
    }
}
