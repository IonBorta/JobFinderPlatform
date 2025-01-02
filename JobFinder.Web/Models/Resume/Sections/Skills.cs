using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.Resume.Sections
{
    public class Skills
    {
        public required string SkillName { get; set; }
        [Range(0, 100)]
        public required int Percentage { get; set; }
    }
}
