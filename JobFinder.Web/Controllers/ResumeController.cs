using System.Text.Json;
using JobFinder.Web.Builder;
using JobFinder.Web.Models.Resume;
using JobFinder.Web.Models.Resume.Sections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using Rotativa.AspNetCore;
using IronPdf;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Glimpse.Mvc.AlternateType;
using IronPdf.Extensions.Mvc.Core;

namespace JobFinder.Web.Controllers
{
    public class ResumeController : Controller
    {
        private CvBuilder _cvBuilder;
        private ResumeViewModel GetResume()
        {
            _cvBuilder = GetBuilderFromSession();
            var resume = _cvBuilder.Build();
            return resume;
        }
        public IActionResult Index()
        {
            _cvBuilder = GetBuilderFromSession();
            var resume = _cvBuilder.Build();
            return View(resume);
        }
        public IActionResult ConvertAndSave()
        {
            _cvBuilder = GetBuilderFromSession();
            var resume = _cvBuilder.Build();

            return new ViewAsPdf("ResumeDetails", resume) // Adjust with your view name and model
            {
                FileName = "CVFromJobFinder.pdf",
                PageSize = Rotativa.AspNetCore.Options.Size.A4,
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                CustomSwitches = "--enable-local-file-access",
            };
        }
        public IActionResult AddPersonalInfo()
        {
            var resume = GetResume();
            var personalInfo = resume.PersonalInfo;
            return View(personalInfo);
        }
        [HttpPost]
        public IActionResult AddPersonalInfo(PersonalInfo model)
        {
            if (ModelState.IsValid)
            {
                _cvBuilder = GetBuilderFromSession();
                _cvBuilder.AddPersonalInfo(model);
                SaveBuilderToSession(_cvBuilder);
                return RedirectToAction("AddWorkExperience");
            }
            return View(model);
        }
        public IActionResult AddWorkExperience()
        {
            var workExperience = GetResume().WorkExperience;
            var model = workExperience.Count > 0 ? workExperience : new List<WorkExperience> { new WorkExperience() };
            return View(model);
        }
        [HttpPost]
        public IActionResult AddWorkExperience(List<WorkExperience> model)
        {
            if (ModelState.IsValid)
            {
                _cvBuilder = GetBuilderFromSession();
                _cvBuilder.AddWorkExperiences(model);
                SaveBuilderToSession(_cvBuilder);
                return RedirectToAction("AddEducation");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult AddEducation()
        {
            var education = GetResume().Education;

            var model = education.Count > 0 ? education : new List<Education> { new Education() };
            return View(model);
        }
        public IActionResult SkipEducation()
        {
            _cvBuilder = GetBuilderFromSession();
            _cvBuilder.AddEducation(new List<Education>());
            SaveBuilderToSession(_cvBuilder);

            return RedirectToAction("AddLanguages");
        }
        [HttpPost]
        public IActionResult AddEducation(List<Education> model)
        {
            if (ModelState.IsValid)
            {
                _cvBuilder = GetBuilderFromSession();
                _cvBuilder.AddEducation(model);
                SaveBuilderToSession(_cvBuilder);
                return RedirectToAction("AddLanguages");
            }
            return View(model);
        }
        public IActionResult AddLanguages()
        {
            var languages = GetResume().Languages;
            var model = languages.Count > 0 ? languages : new List<Languages> { new Languages() };
            return View(model);
        }
        public IActionResult SkipLanguages()
        {
            _cvBuilder = GetBuilderFromSession();
            _cvBuilder.AddLanguages(new List<Languages>());
            SaveBuilderToSession(_cvBuilder);

            return RedirectToAction("AddSkills");
        }
        [HttpPost]
        public IActionResult AddLanguages(List<Languages> model)
        {
            if (ModelState.IsValid)
            {
                _cvBuilder = GetBuilderFromSession();
                _cvBuilder.AddLanguages(model);
                SaveBuilderToSession(_cvBuilder);
                return RedirectToAction("AddSkills");
            }
            return View(model);
        }
        public IActionResult AddSkills()
        {
            var skills = GetResume().Skills;
            var model = skills.Count > 0 ? skills : new List<Skills> { new Skills() };
            return View(model);
        }
        public IActionResult SkipSkills()
        {
            _cvBuilder = GetBuilderFromSession();
            _cvBuilder.AddSkills(new List<Skills>());
            SaveBuilderToSession(_cvBuilder);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult AddSkills(List<Skills> model)
        {
            if (ModelState.IsValid)
            {
                _cvBuilder = GetBuilderFromSession();
                _cvBuilder.AddSkills(model);
                SaveBuilderToSession(_cvBuilder);
                return RedirectToAction("Index");
            }
            return View(model);
        }
        private void SaveBuilderToSession(CvBuilder builder)
        {
            var serializedBuilder = JsonSerializer.Serialize(builder);
            var builderBytes = System.Text.Encoding.UTF8.GetBytes(serializedBuilder);
            HttpContext.Session.Set("CvBuilder", builderBytes); // Save as byte array
        }

        private CvBuilder GetBuilderFromSession()
        {
            HttpContext.Session.TryGetValue("CvBuilder", out var builderBytes);

            if (builderBytes == null)
            {
                return new CvBuilder(); // Return a new instance if no data in session
            }

            var serializedBuilder = System.Text.Encoding.UTF8.GetString(builderBytes);
            return JsonSerializer.Deserialize<CvBuilder>(serializedBuilder) ?? new CvBuilder();
        }
    }
}
