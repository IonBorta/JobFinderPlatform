using AutoMapper;
using JobFinder.BLL.Interfaces;
using JobFinder.BLL.Services;
using JobFinder.Core.Common;
using JobFinder.Core.DTOs.Company;
using JobFinder.Core.Enums;
using JobFinder.Web.Models;
using JobFinder.Web.Models.Company;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JobFinder.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly IJobService _jobService;
        private readonly IApplicationService _applicationService;
        private readonly IMapper _mapper;
        public CompanyController(
            ICompanyService companyService, 
            IJobService jobService, 
            IApplicationService applicationService,
            IMapper mapper
            )
        {
            _companyService = companyService;
            _jobService = jobService;
            _applicationService = applicationService;
            _mapper = mapper;

        }
        // GET: CompanyController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompanyController/Details/5
        public async Task<ActionResult> Details(int id,bool byUserId = false)
        {
            Result<GetCompanyDTO> result = null;
            if (byUserId == true)
            {
                result = await _companyService.GetCompanyByUserId(id, byUserId);
            }
            else
            {
                result = await _companyService.GetById(id);
            }
            if (!result.IsSuccess)
            {
                TempData["Message"] = result.ErrorMessage;
                return View("Error");
            }
            GetCompanyDTO companyDTO = result.Data;
            var company = _mapper.Map<GetCompanyViewModel>(companyDTO);

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
        public async Task<ActionResult> RegisterCompany(CreateCompanyViewModel registerCompanyViewModel)
        {
            if (ModelState.IsValid)
            {
                if(registerCompanyViewModel.Password != registerCompanyViewModel.ConfirmPassword)
                {
                    ModelState.AddModelError("ConfirmPassword", "Password is not the same");
                    return View(registerCompanyViewModel);
                }
                var companyDTO = _mapper.Map<CreateCompanyDTO>(registerCompanyViewModel);
                var result = await _companyService.Add(companyDTO);

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
            var userRole = HttpContext.Session.GetInt32("UserRole");
            if (userRole != (int)UserType.Employer) return Unauthorized();

            var result = await _companyService.GetById(id);
            if (!result.IsSuccess)
            {
                TempData["Message"] = result.ErrorMessage;
                return View("Error");
            }
            GetCompanyDTO companyDTO = result.Data;
            var company = _mapper.Map<EditCompanyViewModel>(companyDTO);

            return View(company);
        }

        // POST: CompanyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(EditCompanyViewModel companyViewModel)
        {
            if (ModelState.IsValid)
            {
                var company = _mapper.Map<UpdateCompanyDTO>(companyViewModel);
                var result = await _companyService.Update(company);
                if (!result.IsSuccess)
                {
                    //ModelState.AddModelError(string.Empty, result.ErrorMessage);
                    TempData["Message"] = result.ErrorMessage; // Pass the error message
                    return View(companyViewModel);
                }
                return RedirectToAction("Details", new { id = companyViewModel.Id });
            }
            return View(companyViewModel);
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
