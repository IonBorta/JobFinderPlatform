using JobFinder.BLL.Interfaces;
using JobFinder.Core.DTOs;
using JobFinder.DAL.Entities;
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
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.LoginUser(loginViewModel.Email, loginViewModel.Password);
                if (!result.IsSuccess)
                {
                    ModelState.AddModelError("Email", result.ErrorMessage);
                    return View(loginViewModel);
                }
                // Stochează informațiile în sesiune
                HttpContext.Session.SetString("UserName", result.Data.Name);
                HttpContext.Session.SetString("UserEmail", result.Data.Email);
                HttpContext.Session.SetInt32("UserId", result.Data.Id);
                HttpContext.Session.SetInt32("UserRole", (int)result.Data.UserType);
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
                var result = await _accountService.AddUser(new UserDTO
                {
                    Name = userViewModel.FullName,
                    Email = userViewModel.Email,
                    Password = userViewModel.Password,
                });

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError("Email", result.ErrorMessage);
                    return View(userViewModel);
                }
            }
            return View(userViewModel);
        }
    }
}
