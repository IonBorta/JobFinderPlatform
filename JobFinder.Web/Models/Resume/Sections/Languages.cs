using System.ComponentModel.DataAnnotations;

namespace JobFinder.Web.Models.Resume.Sections
{
    public class Languages
    {
        [Required]
        public LanguageNames Language { get; set; }
        [Required]
        public LanguageLevels Level { get; set; }
    }
    public class Hobby
    {
        [Required]
        public string Name;
    }
    public enum LanguageNames
    {
        Romanian = 1,
        English,
        Russian,
        French,
        Spanish,
        Ukrainian,
        German
    }
    public enum LanguageLevels
    {
        A1 = 15,
        A2 = 30,
        B1 = 45,
        B2 = 60,
        C1 = 80,
        C2 = 99,
        Native = 100
    }
}
