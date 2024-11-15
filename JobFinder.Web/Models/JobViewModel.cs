using JobFinder.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models
{
    public class JobViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public required string Description { get; set; }
        [Required(ErrorMessage = "Salary is required")]
        public int Salary { get; set; }
        [Required(ErrorMessage = "Requirements are required")]
        public string Requirements { get; set; }
        [Required(ErrorMessage = "Benefits are required")]
        public string Benefits { get; set; }
        [Required(ErrorMessage = "Experience is required")]
        public WorkExperience Experience { get; set; }
/*        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }*/
        [Required(ErrorMessage = "Studies are required")]
        public StudiesLevel Studies { get; set; }
        [Required(ErrorMessage = "WorkingType is required")]
        public WorkingType WorkingType { get; set; }
        [Required(ErrorMessage = "Posted is required")]
        public DateTime Posted { get; set; }

        public string City { get; set; } = string.Empty;
        
        public string CompanyName { get; set; } = string.Empty;
        public int CompanyId { get; set; }
        //public CompanyViewModel Company { get; set; } = new CompanyViewModel();
    }
}
