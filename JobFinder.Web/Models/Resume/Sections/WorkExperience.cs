using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.Resume.Sections
{
    public class WorkExperience
    {
        [Required]
        public string JobName { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Location { get; set; }
        [Required]
        [Range(typeof(DateTime), "2000-01-01", "2030-12-31", ErrorMessage = "Start year must be between 2000 and 2030.")]
        public DateTime StartYear { get; set; } = DateTime.Now;

        [Required]
        [Range(typeof(DateTime), "2000-01-01", "2030-12-31", ErrorMessage = "End year must be between 2000 and 2030.")]
        public DateTime EndYear { get; set; } = DateTime.Now;
    }
}
