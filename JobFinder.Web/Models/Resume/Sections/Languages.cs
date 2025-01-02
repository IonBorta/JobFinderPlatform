using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.Resume.Sections
{
    public class Languages
    {
        public required string Language { get; set; }
        [Range(0, 100)]
        public required int Level { get; set; }
    }
    public class Hobby
    {
        public required string Name;
    }
}
