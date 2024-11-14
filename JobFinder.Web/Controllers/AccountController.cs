using JobFinder.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers
{
    public class AccountController : Controller
    {
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
            return View(userViewModel);
        }
    }
}
