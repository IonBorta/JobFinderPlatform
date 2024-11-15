using JobFinder.BLL.Interfaces;
using JobFinder.Core.DTOs;
using JobFinder.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JobFinder.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly ILogService<CompanyDTO> _logService;
        private readonly IJobService _jobService;
        public CompanyController(ICompanyService companyService, ILogService<CompanyDTO> logService,IJobService jobService)
        {
            _companyService = companyService;
            _logService = logService;
            _jobService = jobService;

        }
        // GET: CompanyController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompanyController/Details/5
        public async Task<ActionResult> Details(int id = 3)
        {
            CompanyDTO companyDTO = await _companyService.GetCompanyById(id);
            var company = new CompanyViewModel()
            {
                Id = companyDTO.Id,
                Name = companyDTO.Name,
                Email = companyDTO.Email,
                Description = companyDTO.Description,
                City = companyDTO.City,
                Domain = companyDTO.Domain,
                Workers = companyDTO.Workers,
                PhoneNumber = companyDTO.PhoneNumber,
            };

            var jobs = await _jobService.GetJobs();
            jobs = jobs.Where(job => job.CompanyId == company.Id).ToList();
            company.Jobs = jobs.Count;

            return View(company);
        }

        // GET: CompanyController/Create
        public ActionResult RegisterCompany()
        {
            return View();
        }

        // POST: CompanyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterCompany(RegisterCompanyViewModel registerCompanyViewModel)
        {
            if (ModelState.IsValid)
            {
                if(registerCompanyViewModel.Password != registerCompanyViewModel.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Password is not the same");
                    return View(registerCompanyViewModel);
                }
                var company = await _logService.GetByEmail(registerCompanyViewModel.Email);
                if(company != null && company.Email == registerCompanyViewModel.Email)
                {
                    ModelState.AddModelError("Email", "This email is already used");
                    return View(registerCompanyViewModel);
                }

                var companyDTO = new CompanyDTO()
                {
                    Name = registerCompanyViewModel.Name,
                    Email = registerCompanyViewModel.Email,
                    PhoneNumber = registerCompanyViewModel.PhoneNumber,
                    Domain = registerCompanyViewModel.Domain,
                    Password = registerCompanyViewModel.Password,
                };
                await _companyService.AddCompany(companyDTO);
                return RedirectToAction("Index","Home");
            }
            return View(registerCompanyViewModel);
        }

        // GET: CompanyController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            CompanyDTO companyDTO = await _companyService.GetCompanyById(id);
            var company = new CompanyViewModel()
            {
                Id = companyDTO.Id,
                Name = companyDTO.Name,
                Email = companyDTO.Email,
                Description = companyDTO.Description,
                City = companyDTO.City,
                Domain = companyDTO.Domain,
                Workers = companyDTO.Workers,
                PhoneNumber = companyDTO.PhoneNumber,
            };

            var jobs = await _jobService.GetJobs();
            jobs = jobs.Where(job => job.CompanyId == company.Id).ToList();
            company.Jobs = jobs.Count;

            return View(company);
        }

        // POST: CompanyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(/*int id, IFormCollection collection*/CompanyViewModel companyViewModel)
        {
            //if(ModelState.IsValid)
            if (companyViewModel.Name == "" || companyViewModel.Email == "" || companyViewModel.Domain == 0 || companyViewModel.Description == "" ||
                    companyViewModel.PhoneNumber == "") return View(companyViewModel);
            var company = new CompanyDTO()
            {
                Id = companyViewModel.Id,
                Name = companyViewModel.Name,
                Email = companyViewModel.Email,
                Description = companyViewModel.Description,
                City = companyViewModel.City,
                Domain = companyViewModel.Domain,
                Workers = companyViewModel.Workers,
                PhoneNumber = companyViewModel.PhoneNumber,

            };
            await _companyService.UpdateCompany(company);
            return RedirectToAction("Details",companyViewModel.Id);
        }

        public IActionResult CompanyJobs(/*IList<JobViewModel> jobs*/)
        {
            if (TempData["JobList"] != null)
            {
                var jobs = JsonConvert.DeserializeObject<List<JobViewModel>>(TempData["JobList"].ToString());
                return View(jobs);
            }
            return View(new List<JobViewModel>());
            //return View(jobs);
        }

        // GET: CompanyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompanyController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
