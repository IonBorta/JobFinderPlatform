using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.Resume.Sections
{
    public class Certifications
    {
        [Required]
        public string CertificationName { get; set; }
        [Required]
        public DateTime Year { get; set; }
        [Required]
        public string From { get; set; }
    }
}
