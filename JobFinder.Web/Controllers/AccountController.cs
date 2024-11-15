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
/*                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, _configuration["JWT.Subject"]),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    //new Claim(ClaimTypes.NameIdentifier, user!.Id.ToString()),
                    new Claim(ClaimTypes.Name, user!.Name),
                    new Claim(ClaimTypes.Email, user!.Email)
                };*/
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    //claims: claims,
                    expires: DateTime.Now.AddHours(1),
                    signingCredentials: creds
                );
                string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                // Save the token in TempData
                TempData["Token"] = tokenValue;
                TempData["UserName"] = user.Name;
                TempData["Role"] = user.UserType;

                return RedirectToAction("Index", "Home");
            }
            return View(loginViewModel);
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
            if (pdfFile != null && pdfFile.Length > 0)
            {

                using (var memoryStream = new MemoryStream())
                {
                    await pdfFile.CopyToAsync(memoryStream); // Citește fișierul în memorie

                    var application = new ApplicationDTO
                    {
                        JobId = jobId,
                        //UserId = GetUserId(), // O metodă care obține ID-ul utilizatorului curent
                        FileContent = memoryStream.ToArray(), // Convertește fluxul în array de bytes
                        FileName = pdfFile.FileName, // Numele fișierului
                        ContentType = pdfFile.ContentType // Tipul MIME
                    };

                    await _applicationService.AddApplication(application); // Salvare în baza de date
                }

                TempData["Message"] = "Application submitted successfully!";
                return RedirectToAction("Jobs","Job");
            }

            TempData["Message"] = "File upload failed.";
            return RedirectToAction("JobDetails"); // redirect to the same or another page
        }
    }
}
