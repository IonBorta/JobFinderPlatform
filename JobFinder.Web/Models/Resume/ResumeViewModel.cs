using JobFinder.Web.Models.Resume.Sections;

namespace JobFinder.Web.Models.Resume
{
    public class ResumeViewModel
    {
        public PersonalInfo PersonalInfo { get; set; } = new PersonalInfo();
        public List<WorkExperience> WorkExperience { get; set; } = new List<WorkExperience>();
        public List<Education> Education { get; set; } = new List<Education>();
        public List<Skills> Skills { get; set; } = new List<Skills>();
        public List<Languages> Languages { get; set; } = new List<Languages>();
        public List<Hobby> Hobbies { get; set; } = new List<Hobby>();
        public List<Certifications> Certifications { get; set; } = new List<Certifications>();
    }
}
