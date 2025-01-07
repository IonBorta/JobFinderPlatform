using System.Text.Json;
using System.Text.Json.Serialization;
using JobFinder.Web.Models.Resume;
using JobFinder.Web.Models.Resume.Sections;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace JobFinder.Web.Builder
{
    public class CvBuilder : ICvBuilder
    {
        [JsonInclude]
        private ResumeViewModel _cv = new();

        public ICvBuilder AddPersonalInfo(PersonalInfo info)
        {
            _cv.PersonalInfo = info;
            return this;
        }

        public ICvBuilder AddWorkExperiences(List<WorkExperience> experiences)
        {
            _cv.WorkExperience = experiences;
            return this;
        }

        public ICvBuilder AddEducation(List<Education> education)
        {
            _cv.Education = education;
            return this;
        }

        public ICvBuilder AddSkills(List<Skills> skills)
        {
            _cv.Skills = skills;
            return this;
        }

        public ICvBuilder AddCertifications(List<Certifications> certifications)
        {
            _cv.Certifications = certifications;
            return this;
        }
        public ICvBuilder AddLanguages(List<Languages> languages)
        {
            _cv.Languages = languages;
            return this;
        }
        public ResumeViewModel Build() => _cv;
    }

}
