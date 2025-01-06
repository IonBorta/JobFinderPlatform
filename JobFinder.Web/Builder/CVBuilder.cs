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
        //private readonly HttpContext _httpContext;
/*        public CvBuilder(HttpContext httpContext)
        {
            _httpContext = httpContext;
        }*/

        public ICvBuilder AddPersonalInfo(PersonalInfo info)
        {
            _cv.PersonalInfo = info;
            //SaveCVToSession();
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

        /*
       private void SaveCVToSession()
       {
           var serializedCv = JsonSerializer.Serialize(_cv);
           var cvBytes = System.Text.Encoding.UTF8.GetBytes(serializedCv);
           _httpContext.Session.Set("Cv", cvBytes); // Save as byte array
       }

       private ResumeViewModel GetCVFromSession()
       {
           _httpContext.Session.TryGetValue("Cv", out var builderBytes);

           if (builderBytes == null)
           {
               return new ResumeViewModel(); // Return a new instance if no data in session
           }

           var serializedBuilder = System.Text.Encoding.UTF8.GetString(builderBytes);
           return JsonSerializer.Deserialize<ResumeViewModel>(serializedBuilder) ?? new ResumeViewModel();
       }*/
    }

}
