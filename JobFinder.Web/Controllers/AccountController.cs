using JobFinder.BLL.Interfaces;
using JobFinder.Core.DTOs;
using JobFinder.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILogService<UserDTO> _logService;
        public AccountController(IAccountService accountService, ILogService<UserDTO> logService)
        {
            _accountService = accountService;
            _logService = logService;

        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
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
        public IActionResult RegisterUser(UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                if(userViewModel.Password != userViewModel.ConfirmPassword)
                {
                    return View(userViewModel);
                }
                var user = _logService.GetByEmail(userViewModel.Email);
                if (user != null)
                {
                    return View(userViewModel);
                }
                var userDTO = new UserDTO()
                {
                    Name = userViewModel.FullName,
                    Email = userViewModel.Email,
                    Password = userViewModel.Password,
                };
                _accountService.AddUser(userDTO);
            }
            return View(userViewModel);
        }
    }
}
