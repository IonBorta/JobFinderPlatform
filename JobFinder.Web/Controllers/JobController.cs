using JobFinder.BLL.Interfaces;
using JobFinder.BLL.Services;
using JobFinder.Core.DTOs;
using JobFinder.DAL.Repositories;
using JobFinder.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace JobFinder.Web.Controllers
{
    public class JobController : Controller
    {
        private readonly IJobService _jobService;
        private readonly ICompanyService _companyService;
        //private int companyId = 0;

        public JobController(IJobService jobService, ICompanyService companyService)
        {
            _jobService = jobService;
            _companyService = companyService;
        }
        public async Task<IActionResult> Jobs(int companyId = 0)
        {


            var jobDTOs = await _jobService.GetJobs();

            if(companyId != 0)
            {
                jobDTOs = jobDTOs.Where(job => job.CompanyId == companyId).ToList();
            }

            var jobViewModels = jobDTOs.Select(job => new JobViewModel()
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Requirements = job.Requirements,
                Benefits = job.Benefits,
                Salary = job.Salary,
                Experience = job.Experience,
                //City = companyCity,
                Studies = job.Studies,
                WorkingType = job.WorkingType,
                CompanyId = job.CompanyId
            }).ToList();
            foreach (var job in jobViewModels)
            {
                var company = await _companyService.GetCompanyById(job.CompanyId);
                job.CompanyName = company.Name;
                job.City = company.City;
            }

            if(companyId != 0)
            {
                TempData["JobList"] = JsonConvert.SerializeObject(jobViewModels);
                return RedirectToAction("CompanyJobs", "Company");
                //return RedirectToAction("CompanyJobs", "Company", new { jobs = jobViewModels });
            }

            return View(jobViewModels);
        }
        public async Task<IActionResult> JobDetails(int id)
        {
            var jobDTO = await _jobService.GetJobById(id);
            var job = new JobViewModel()
            {
                Title = jobDTO.Title,
                Description = jobDTO.Description,
                Requirements = jobDTO.Requirements,
                Benefits = jobDTO.Benefits,
                Salary = jobDTO.Salary,
                Experience = jobDTO.Experience,
                Studies = jobDTO.Studies,
                WorkingType = jobDTO.WorkingType,
                CompanyId = jobDTO.CompanyId,
                Posted = jobDTO.Posted
            };

            var company = await _companyService.GetCompanyById(job.CompanyId);
            job.CompanyName = company.Name;
            job.City = company.City;

            return View(job);
        }
        [HttpGet]
        public IActionResult AddJob(int id)
        {
            TempData["CompanyId"] = id;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddJobAsync(JobViewModel jobViewModel)
        {
            if (ModelState.IsValid)
            {
                int companyId = Convert.ToInt32(TempData["CompanyId"]);
                var jobDto = new JobDTO()
                {
                    Title = jobViewModel.Title,
                    Description = jobViewModel.Description,
                    Requirements = jobViewModel.Requirements,
                    Benefits    = jobViewModel.Benefits,
                    Salary = jobViewModel.Salary,
                    Experience = jobViewModel.Experience,
                    //City = jobViewModel.City,
                    Studies = jobViewModel.Studies,
                    WorkingType = jobViewModel.WorkingType,
                    CompanyId = companyId
            };
                //var company = await _logService.GetByEmail("utm@gmail.com");
                //jobDto.CompanyId = company.Id;

                await _jobService.AddJob(jobDto);

                return RedirectToAction("Jobs");
            }
            return View(jobViewModel);
        }
        public async Task<ActionResult> Edit(int id)
        {
            var jobDTO = await _jobService.GetJobById(id);
            var job = new JobViewModel()
            {
                Id = jobDTO.Id,
                Title = jobDTO.Title,
                Description = jobDTO.Description,
                Requirements = jobDTO.Requirements,
                Benefits= jobDTO.Benefits,
                Salary = jobDTO.Salary,
                Experience = jobDTO.Experience,
                WorkingType= jobDTO.WorkingType,
                Studies= jobDTO.Studies,
            };
            return View(job);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(JobViewModel jobViewModel)
        {
            //if(ModelState.IsValid)
            /*            if (companyViewModel.Name == "" || companyViewModel.Email == "" || companyViewModel.Domain == 0 || companyViewModel.Description == "" ||
                                companyViewModel.PhoneNumber == "") return View(companyViewModel);*/
            if (ModelState.IsValid)
            {
                var job = new JobDTO(){
                    Id = jobViewModel.Id,
                    Title = jobViewModel.Title,
                    Description = jobViewModel.Description,
                    Requirements = jobViewModel.Requirements,
                    Benefits = jobViewModel.Benefits,
                    Salary = jobViewModel.Salary,
                    Experience = jobViewModel.Experience,
                    WorkingType = jobViewModel.WorkingType,
                    Studies = jobViewModel.Studies,
                };
                await _jobService.UpdateJob(job);
                return RedirectToAction("JobDetails", new {id = job.Id});
            }
            return View(jobViewModel);
        }
    }
}
