using JobFinder.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models
{
    public class JobViewModel
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public required string Description { get; set; }
        [Required(ErrorMessage = "Salary is required")]
        public decimal Salary { get; set; }
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
    }
}
