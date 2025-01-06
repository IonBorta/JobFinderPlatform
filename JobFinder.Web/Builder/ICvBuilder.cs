using JobFinder.Web.Models.Resume;
using JobFinder.Web.Models.Resume.Sections;

namespace JobFinder.Web.Builder
{
    public interface ICvBuilder
    {
        ICvBuilder AddPersonalInfo(PersonalInfo info);
        ICvBuilder AddWorkExperiences(List<WorkExperience> experiences);
        ICvBuilder AddEducation(List<Education> education);
        ICvBuilder AddLanguages(List<Languages> languages);
        ICvBuilder AddSkills(List<Skills> skills);
        ICvBuilder AddCertifications(List<Certifications> certifications);
        ResumeViewModel Build();
    }

}
