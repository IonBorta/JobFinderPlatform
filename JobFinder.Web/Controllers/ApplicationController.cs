using JobFinder.BLL.Interfaces;
using JobFinder.BLL.Services;
using JobFinder.Core.DTOs;
using JobFinder.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;
        public ApplicationController(IApplicationService applicationService)
        {
            _applicationService = applicationService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Apply(int jobId, IFormFile pdfFile)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login","Account");
            if (pdfFile != null && pdfFile.Length > 0)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await pdfFile.CopyToAsync(memoryStream); // Citește fișierul în memorie

                    var application = new ApplicationDTO
                    {
                        JobId = jobId,
                        UserId = (int)userId,
                        UserName = HttpContext.Session.GetString("UserName")!,
                        FileContent = memoryStream.ToArray(), // Convertește fluxul în array de bytes
                        FileName = pdfFile.FileName, // Numele fișierului
                        ContentType = pdfFile.ContentType // Tipul MIME
                    };

                    
                    var result = await _applicationService.AddApplication(application);
                    if (!result.IsSuccess)
                    {
                        ModelState.AddModelError(string.Empty, result.ErrorMessage);
                        return RedirectToAction("JobDetails","Job",new {id =  jobId});
                    }
                }
                TempData["Message"] = "Application submitted successfully!";
                return RedirectToAction("UserApplications");
            }

            TempData["Message"] = "File upload failed.";
            return RedirectToAction("JobDetails", "Job", new { id = jobId });
        }
        public async Task<IActionResult> UserApplications()
        {
            var applications = await _applicationService.GetApplications();
            var viewmodels = applications.Select(app => new ApplicationViewModel()
            {
                Id = app.Id,
                CompanyName = app.CompanyName,
                UserEmail = app.UserEmail,
                UserName = app.UserName,
                JobName = app.JobName,
                Submited = app.Submited,
            }).ToList();
            return View(viewmodels);
        }
        public async Task<IActionResult> CompanyApplications(int id)
        {
            var dtos = await _applicationService.GetApplicationsByCompany(id);
            var models = dtos.Select(dto => new ApplicationViewModel()
            {
                Id = dto.Id,
                CompanyName = dto.CompanyName,
                UserEmail = dto.UserEmail,
                UserName = dto.UserName,
                JobName = dto.JobName,
                Submited = dto.Submited,
            }).ToList();
            return View(models);
        }
        public async Task<IActionResult> ViewPDF(int id)
        {
            var result = await _applicationService.GetApplcationById(id);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View("Error");
            }
            var application = result.Data;

            if (application != null)
            {
                return File(application.FileContent, application.ContentType);
            }

            return NotFound();
        }
    }
}
