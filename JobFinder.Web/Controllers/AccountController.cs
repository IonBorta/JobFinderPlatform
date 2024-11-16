using JobFinder.BLL.Interfaces;
using JobFinder.Core.DTOs;
using JobFinder.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JobFinder.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IApplicationService _applicationService;
        private readonly ILogService<UserDTO> _logService;
        private readonly IConfiguration _configuration;
        public AccountController(IAccountService accountService, ILogService<UserDTO> logService, IApplicationService applicationService, IConfiguration configuration)
        {
            _accountService = accountService;
            _logService = logService;
            _applicationService = applicationService;
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            //ViewBag.UserName = TempData["UserName"]?.ToString();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var user = await _logService.GetByEmail(loginViewModel.Email);
            if(user != null)
            {
                // Stochează informațiile în sesiune
                HttpContext.Session.SetString("UserName", user.Name);
                HttpContext.Session.SetString("UserEmail", user.Email);
                HttpContext.Session.SetInt32("UserId", user.Id);
                HttpContext.Session.SetInt32("UserRole", (int)user.UserType);

                return RedirectToAction("Index", "Home");
            }
            return View(loginViewModel);
        }
        public IActionResult Logout()
        {
            // Șterge informațiile de sesiune
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
        public IActionResult Register()
        {
            return View();
        }
        public IActionResult RegisterUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> RegisterUser(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                if(userViewModel.Password != userViewModel.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Password is not the same");
                    return View(userViewModel);
                }
                var user = await _logService.GetByEmail(userViewModel.Email);
                if (user != null && userViewModel.Email == user.Email)
                {
                    ModelState.AddModelError("Email", "This email is already used.");
                    return View(userViewModel);
                }
                var userDTO = new UserDTO()
                {
                    Name = userViewModel.FullName,
                    Email = userViewModel.Email,
                    Password = userViewModel.Password,
                };
                await _accountService.AddUser(userDTO);
                RedirectToAction("Index","Home");
            }
            return View(userViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Apply(int jobId,IFormFile pdfFile)
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null) return RedirectToAction("Login");
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

                    await _applicationService.AddApplication(application); // Salvare în baza de date
                }

                TempData["Message"] = "Application submitted successfully!";
                return RedirectToAction("MyApplications","Account");
            }

            TempData["Message"] = "File upload failed.";
            return RedirectToAction("JobDetails"); // redirect to the same or another page
        }
        public async Task<IActionResult> MyApplications()
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
        public async Task<IActionResult> ViewPDF(int id)
        {
            var application = await _applicationService.GetApplcationById(id);

            if (application != null)
            {
                return File(application.FileContent, application.ContentType);
            }

            return NotFound();
        }
    }
}
