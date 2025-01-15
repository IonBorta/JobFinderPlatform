using AutoMapper;
using JobFinder.BLL.Interfaces;
using JobFinder.Core.DTOs.User;
using JobFinder.DAL.Entities;
using JobFinder.Web.Models;
using JobFinder.Web.Models.Auth;
using JobFinder.Web.Models.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
            return await LoginUser(loginViewModel);
            /*            if (ModelState.IsValid)
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
                        return View(loginViewModel);*/
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
        public async Task<IActionResult> RegisterUser(CreateUserViewModel createUserViewModel)
        {
            if (ModelState.IsValid)
            {
                if(createUserViewModel.Password != createUserViewModel.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Password is not the same");
                    return View(createUserViewModel);
                }
                var userDTO = _mapper.Map<CreateUserDTO>(createUserViewModel);
                var result = await _accountService.Add(userDTO);

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError("Email", result.ErrorMessage);
                    return View(createUserViewModel);
                }
                //return RedirectToAction("Login");
                return await LoginUser(new LoginViewModel() { Email = createUserViewModel.Email, Password = createUserViewModel.Password });
            }
            return View(createUserViewModel);
        }
        private async Task<IActionResult> LoginUser(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.LoginUser(model.Email, model.Password);
                if (!result.IsSuccess)
                {
                    ModelState.AddModelError("Email", result.ErrorMessage);
                    return View("Login",model);
                }
                // Stochează informațiile în sesiune
                HttpContext.Session.SetString("UserName", result.Data.Name);
                HttpContext.Session.SetString("UserEmail", result.Data.Email);
                HttpContext.Session.SetInt32("UserId", result.Data.Id);
                HttpContext.Session.SetInt32("UserRole", (int)result.Data.UserType);
                return RedirectToAction("Index", "Home");
            }
            return View("Login", model);
        }
    }
}
