using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.Resume.Sections
{
    public class Education
    {
        public required string SchoolName { get; set; }
        public required string Specialty {  get; set; }
        public required string Degree {  get; set; }
        public required DateTime StartYear{ get; set; }
        public required DateTime EndYear { get; set; }
    }
}
