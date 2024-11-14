using JobFinder.BLL.Interfaces;
using JobFinder.Core.DTOs;
using JobFinder.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JobFinder.Web.Controllers
{
    public class CompanyController : Controller
    {
        private readonly ICompanyService _companyService;
        private readonly ILogService<CompanyDTO> _logService;
        public CompanyController(ICompanyService companyService, ILogService<CompanyDTO> logService)
        {
            _companyService = companyService;
            _logService = logService;

        }
        // GET: CompanyController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompanyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompanyController/Create
        public ActionResult RegisterCompany()
        {
            return View();
        }

        // POST: CompanyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RegisterCompany(RegisterCompanyViewModel registerCompanyViewModel)
        {
            if (ModelState.IsValid)
            {
                if(registerCompanyViewModel.Password != registerCompanyViewModel.ConfirmPassword)
                {
                    return View(registerCompanyViewModel);
                }
                var company = _logService.GetByEmail(registerCompanyViewModel.Email);
                if(company != null)
                {
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
                _companyService.AddCompany(companyDTO);
                RedirectToAction("Details");
            }
            return View(registerCompanyViewModel);
/*            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }*/
        }

        // GET: CompanyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompanyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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
