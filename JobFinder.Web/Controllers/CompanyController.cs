using JobFinder.BLL.Interfaces;
using JobFinder.BLL.Services;
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
        private readonly IApplicationService _applicationService;
        public CompanyController(ICompanyService companyService, ILogService<CompanyDTO> logService,IJobService jobService, IApplicationService applicationService)
        {
            _companyService = companyService;
            _logService = logService;
            _jobService = jobService;
            _applicationService = applicationService;

        }
        // GET: CompanyController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompanyController/Details/5
        public async Task<ActionResult> Details(int id,bool byUserId = false)
        {
            var result = await _companyService.GetCompanyById(id,byUserId);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View("Error");
            }
            CompanyDTO companyDTO = result.Data;
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
                Jobs = companyDTO.JobsCount
            };

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
                var result = await _companyService.AddCompany(new CompanyDTO
                {
                    Name = registerCompanyViewModel.Name,
                    Email = registerCompanyViewModel.Email,
                    PhoneNumber = registerCompanyViewModel.PhoneNumber,
                    Domain = registerCompanyViewModel.Domain,
                    Password = registerCompanyViewModel.Password,
                });

                if (!result.IsSuccess)
                {
                    ModelState.AddModelError("Email", result.ErrorMessage);
                    return View(registerCompanyViewModel);
                }
                return RedirectToAction("Index","Home");
            }
            return View(registerCompanyViewModel);
        }

        // GET: CompanyController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var result = await _companyService.GetCompanyById(id);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View("Error");
            }
            CompanyDTO companyDTO = result.Data;
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
                Jobs =  companyDTO.JobsCount
            };

            return View(company);
        }

        // POST: CompanyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(CompanyViewModel companyViewModel)
        {
            if (companyViewModel.Name == "" || companyViewModel.Email == "" ||
                companyViewModel.Domain == 0 || companyViewModel.Description == "" ||
                    companyViewModel.PhoneNumber == "")
            {
                return View(companyViewModel);
            }
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
            var result = await _companyService.UpdateCompany(company);
            if (!result.IsSuccess)
            {
                ModelState.AddModelError(string.Empty, result.ErrorMessage);
                return View(companyViewModel);
            }
            return RedirectToAction("Details",companyViewModel.Id);
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
