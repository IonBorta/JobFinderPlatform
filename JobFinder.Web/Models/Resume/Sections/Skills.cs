using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.Resume.Sections
{
    public class Skills
    {
        [Required]
        public string SkillName { get; set; }
        [Required]
        [Range(20, 100)]
        public int Level { get; set; } = 20;
    }
}
