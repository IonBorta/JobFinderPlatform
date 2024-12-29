using System.ComponentModel.Design;
using AutoMapper;
using JobFinder.BLL.Interfaces;
using JobFinder.BLL.Services;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs;
using JobFinder.Core.Enums;
using JobFinder.DAL.Entities;
using JobFinder.Web.Models;
using JobFinder.Web.Models.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers
{
    public class ApplicationController : Controller
    {
        private readonly IApplicationService _applicationService;
        private readonly ICompanyService _companyService;
        private readonly IMapper _mapper;
        public ApplicationController(
            IApplicationService applicationService, 
            IMapper mapper,
            ICompanyService companyService)
        {
            _applicationService = applicationService;
            _mapper = mapper;
            _companyService = companyService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CheckLogin()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Json(new { isLoggedIn = false, loginUrl = Url.Action("Login", "Account") });
            }

            return Json(new { isLoggedIn = true });
        }
        [HttpPost]
        public async Task<IActionResult> Apply(int jobId, int companyId, IFormFile pdfFile, int applicationId = -1)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            var userRole = HttpContext.Session.GetInt32("UserRole");

            if (userId == null) return RedirectToAction("Login", "Account");

            if (userRole != (int)UserType.Employee) return Unauthorized();

            if (pdfFile != null && pdfFile.Length > 0)
            {
                Result result = null;
                if (applicationId == -1)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await pdfFile.CopyToAsync(memoryStream); // Citește fișierul în memorie
                        var application = new ApplicationDTO
                        {
                            Created = DateTime.Now,
                            JobId = jobId,
                            UserId = (int)userId,
                            CompanyId = companyId,
                            UserName = HttpContext.Session.GetString("UserName")!,
                            FileContent = memoryStream.ToArray(), // Convertește fluxul în array de bytes
                            FileName = pdfFile.FileName, // Numele fișierului
                            ContentType = pdfFile.ContentType // Tipul MIME
                        };
                        result = await _applicationService.Add(application);
                    }
                }
                else
                {
                    result = await _applicationService.Reaply(applicationId, pdfFile);
                }
                if (!result.IsSuccess)
                {
                    TempData["Message"] = result.ErrorMessage;
                    return RedirectToAction("JobDetails", "Job", new { id = jobId });
                }

                TempData["Message"] = "Application submitted successfully!";
                return RedirectToAction("UserApplications");
            }

            TempData["Message"] = "File upload failed.";
            return RedirectToAction("JobDetails", "Job", new { id = jobId });
        }
        public async Task<IActionResult> UserApplications()
        {
            var userRole = HttpContext.Session.GetInt32("UserRole");
            if (userRole != (int)UserType.Employee) return Unauthorized();

            var currentUserId = HttpContext.Session.GetInt32("UserId");

            var applications = await _applicationService.GetAll();
            applications = applications.Where(app => app.UserId == currentUserId).ToList();
            var viewmodels = applications.Select(app => _mapper.Map<GetApplicationViewModel>(app)).ToList();
            return View(viewmodels);
        }
        public async Task<IActionResult> CompanyApplications(int id)
        {
            var userRole = HttpContext.Session.GetInt32("UserRole");
            if (userRole != (int)UserType.Employer) return Unauthorized();

            var dtos = await _applicationService.GetApplicationsByCompany(id);
            var models = dtos.Select(dto => _mapper.Map<GetApplicationViewModel>(dto)).ToList();
            return View(models);
        }
        public async Task<IActionResult> ViewPDF(int id,int userId, int companyId)
        {
            var userRole = HttpContext.Session.GetInt32("UserRole");
            if (userRole == null) return RedirectToAction("Login", "Account");

            if (userRole == (int)UserType.Employer)
            {
                var authorization = await AuthorizeCompany(userId, companyId);
                if (authorization != null) return authorization;

                var updateResult = await _applicationService.See(id);
                if (!updateResult.IsSuccess)
                {
                    TempData["Message"] = updateResult.ErrorMessage; // Pass the error message
                    return NotFound(updateResult.ErrorMessage);
                }
                var application = updateResult.Data;
                if (application == null) return NotFound();
                return File(application.FileContent, application.ContentType);
            }
            else
            {
                var authorization = AuthorizeUser(userId);
                if (authorization != null) return authorization;

                var result = await _applicationService.GetById(id);
                if (!result.IsSuccess)
                {
                    TempData["Message"] = result.ErrorMessage;
                    return NotFound(result.ErrorMessage);
                }
                var application = result.Data;

                if (application == null) return NotFound();
                return File(application.FileContent, application.ContentType);
            }
        }
        public async Task<IActionResult> Withdraw(int id, int userId)
        {
            var authorization = AuthorizeUser(userId);
            if (authorization != null) return authorization;

            var result = await _applicationService.Withdraw(id);

            if (!result.IsSuccess)
            {
                TempData["Message"] = result.ErrorMessage; // Pass the error message
            }
            return RedirectToAction("UserApplications");
        }
        public async Task<IActionResult> Answer(int id, int state, int userId, int companyId)
        {
            var authorization = await AuthorizeCompany(userId, companyId);
            if (authorization != null) return authorization;

            var result = await _applicationService.Answer(id, state);

            if (!result.IsSuccess)
            {
                TempData["Message"] = result.ErrorMessage; // Pass the error message
            }
            return RedirectToAction("CompanyApplications", new { id = companyId });
        }
        public async Task<IActionResult> Accept(int id, int userId,int companyId)
        {
            return await Answer(id, (int)ApplicationJobStates.Accepted,userId,companyId);
        }
        public async Task<IActionResult> Reject(int id, int userId, int companyId)
        {
            return await Answer(id, (int)ApplicationJobStates.Rejected, userId, companyId);
        }
        private IActionResult AuthorizeUser(int userId)
        {
            var userRole = HttpContext.Session.GetInt32("UserRole");
            var currentUserId = HttpContext.Session.GetInt32("UserId");
            if (userRole != (int)UserType.Employee && currentUserId != userId) return Unauthorized();
            return null;
        }
        private async Task<IActionResult> AuthorizeCompany(int userId,int companyId)
        {
            var userRole = HttpContext.Session.GetInt32("UserRole");
            var currentUserId = HttpContext.Session.GetInt32("UserId");
            if (userRole != (int)UserType.Employer && currentUserId != userId) return Unauthorized();

            var companyResult = await _companyService.GetCompanyByUserId(currentUserId ?? -1);
            if (companyResult != null && companyResult.IsSuccess)
            {
                var company = companyResult.Data;
                if (company != null && company.Id != companyId)
                {
                    return Unauthorized();
                }
                else if(company == null) return NotFound();
            }
            else if(companyResult == null) return NotFound();
            return null;
        }
    }
}
