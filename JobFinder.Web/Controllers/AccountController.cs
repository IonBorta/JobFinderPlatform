using AutoMapper;
using JobFinder.BLL.Interfaces;
using JobFinder.Core.DTOs;
using JobFinder.DAL.Entities;
using JobFinder.Web.Models;
using JobFinder.Web.Models.Auth;
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
        private readonly IMapper _mapper;
        public AccountController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
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
        public async Task<IActionResult> RegisterUser(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                if(registerViewModel.Password != registerViewModel.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Password is not the same");
                    return View(registerViewModel);
                }
                var userDTO = _mapper.Map<UserDTO>(registerViewModel);
                var result = await _accountService.AddUser(userDTO);

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError("Email", result.ErrorMessage);
                    return View(registerViewModel);
                }
            }
            return View(registerViewModel);
        }
    }
}
