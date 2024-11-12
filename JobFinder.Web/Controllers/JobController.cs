using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers
{
    public class JobController : Controller
    {
        public IActionResult Jobs()
        {
            return View();
        }
        public IActionResult JobDetails()
        {
            return View();
        }
    }
}
