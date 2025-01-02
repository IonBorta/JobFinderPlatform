using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.Resume.Sections
{
    public class WorkExperience
    {
        public required string JobName { get; set; }
        public required string Description { get; set; }
        public required string Location { get; set; }
        public required DateTime StartYear { get; set; }
        public required DateTime EndYear { get; set; }
    }
}
