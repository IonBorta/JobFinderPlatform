using JobFinder.Web.Models.Resume;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers
{
    public class ResumeController : Controller
    {
        public IActionResult Index()
        {
            return View(new ResumeViewModel());
        }
    }
}
